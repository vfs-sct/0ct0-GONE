//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventoryV2 : MonoBehaviour
{
    [SerializeField] UIAwake UIRoot = null;
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] GameObject ResourceBox = null;
    [SerializeField] GameObject MassHUD = null;
    [SerializeField] HorizontalLayoutGroup RowOne = null;
    [SerializeField] HorizontalLayoutGroup RowTwo = null;
    //place item in the item inventory screen
    [SerializeField] GameObject defaultInventoryItem = null;

    [SerializeField] HUDInventoryWidget InventoryWidget = null;
    [SerializeField] EventSystem eventSystem = null;

    [SerializeField] Button[] tabs;
    [SerializeField] GameObject[] tabContent;

    [SerializeField] VerticalLayoutGroup[] inventoryVertRows = null;

    private InventoryController playerInventory = null;
    private Transform debrisDropPos;
    //association between a resource box and the resource it's displaying
    private Dictionary<Resource, GetObjectsResourceBox> ResourceBoxes = new Dictionary<Resource, GetObjectsResourceBox>();

    //save an association between the chunk and the specific item that's filling it in - multiple chunks can use the same item
    private Dictionary<Button, Item> ItemButtonAssociation = new Dictionary<Button, Item>();
    
    //used to display how octo's speed is changed by how much he's carrying
    private TextMeshProUGUI massText;
    private TextMeshProUGUI speedModText;

    private int currentTotalMass;

    private bool[] isActive = new bool[10]
    {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
    };

    private void Awake()
    {
        for(int i = 0; i < tabs.Length; i++)
        {
            //set up which tab should be active first
            if (i == 0)
            {
                tabs[i].interactable = false;
                tabContent[i].SetActive(true);
            }
            else
            {
                tabs[i].interactable = true;
                tabContent[i].SetActive(false);
            }
        }
    }

    public void SwitchTab(int index)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if (i == index)
            {
                tabs[i].interactable = false;
                tabContent[i].SetActive(true);
            }
            else
            {
                tabs[i].interactable = true;
                tabContent[i].SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(UIRoot.GetPlayer() == null)
        {
            Debug.LogError("NULL PLAYER");
            return;
        }
        //playerInventory = UIRoot.GetPlayer().GetComponent<InventoryController>();
        PopulateResources();
        UpdateAllChunks();
        AddMassHUD();
    }

    // Update is called once per frame
    void Update()
    {
        currentTotalMass = 0;
        foreach(var kvp in ResourceBoxes)
        {
            int currentAmount = playerInventory.GetResourceAmount(kvp.Key);
            float fillAmount = (currentAmount / 10);
            kvp.Value.GetCapacityText().SetText($"Capacity:\n{fillAmount}/10");

            currentTotalMass += currentAmount;
        }

        UpdateMassHUD();
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        AkSoundEngine.PostEvent("MainMenu_All_Button_Hover", gameObject);
        if(playerInventory == null)
        {
            playerInventory = UIRoot.GetPlayer().GetComponent<InventoryController>();
            debrisDropPos = UIRoot.GetPlayer().GetComponentInChildren<SatelliteInventory>().SatelliteSpawnPos;
        }
        if (playerInventory.CheckIfItemBucket())
        {
            PopulateItemInventory();
        }
        UpdateAllChunks();
    }

    private void OnDisable()
    {
        Cursor.visible = false;
    }

    public void OnEsc(InputValue value)
    {
        Close();
    }

    public void OnInventoryHotkey(InputValue value)
    {
        Close();
    }

    public void Close()
    {
        AkSoundEngine.PostEvent("MainMenu_All_Button_Hover", gameObject);
        GameManager.UnPause();
        gameObject.SetActive(false);
    }

    public void UpdateAllChunks()
    {
        //for each resource type
        foreach(var kvp in ResourceBoxes)
        {
            //turn everyone off first
            foreach(var button in kvp.Value.GetChunkButtons())
            {
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                button.SetActive(false);
            }
            for(int l = 0; l < isActive.Length; l++)
            {
                isActive[l] = false;
            }

            var bucket = playerInventory.GetResourceBucket(kvp.Key);

            foreach (var item in bucket.Bucket)
            {
                Debug.Log(item.Key.name);
                Debug.Log(item.Value);
            }

            //Debug.Log("BUCKET COUNT" + bucket.Bucket.Count);
            //get all the items in the bucket
            foreach (var item in bucket.Bucket)
            {
                for (int instances = 0; instances < item.Value; instances++)
                {
                    //Debug.Log("HEY" + item.Size / 10);
                    //figure out if the item takes more than 1 slot
                    float chunkSize = (float)item.Key.Size / 10;
                    int j = item.Key.Size / 10;
                    if (j != 0)
                    {
                        //assign the appropriate number of slots for that item
                        for (int k = 0; k < isActive.Length && j != 0; k++)
                        {
                            if (isActive[k] == false)
                            {
                                kvp.Value.SetChunkBool(k, true);
                                kvp.Value.GetChunkButtons()[k].SetActive(true);
                                kvp.Value.SetChunkTooltip(k, item.Key.Name, chunkSize.ToString() + " Slots");
                                kvp.Value.GetChunkButtons()[k].GetComponent<Button>().onClick.AddListener(() =>
                                {
                                    //Debug.Log("CHUNK CLICKED!");
                                    Instantiate(item.Key.RespawnGO).transform.position = debrisDropPos.position;
                                    playerInventory.RemoveFromResourceBucket(item.Key);
                                    UpdateAllChunks();
                                });

                                isActive[k] = true;
                                j--;
                            }
                        }
                    }
                }
            }
        }
    }
    private void PopulateItemInventory()
    {
        //Debug.LogWarning(playerInventory.name);
        
        int childCount = inventoryVertRows[0].transform.childCount;
        //remove old items, row 1
        for (int i = 0; i < childCount; i++)
        {
            Destroy(inventoryVertRows[0].transform.GetChild(i).gameObject);
        }

        childCount = inventoryVertRows[1].transform.childCount;
        //remove old items, row 2
        for (int i = 0; i < childCount; i++)
        {
            Destroy(inventoryVertRows[1].transform.GetChild(i).gameObject);
        }

        bool isFirstRow = true;

        foreach (var kvp in playerInventory.GetItemBucket()[0].Bucket)
        {
            var newItemBox = Instantiate(defaultInventoryItem);
            if (isFirstRow == true)
            {
                newItemBox.transform.SetParent(inventoryVertRows[0].transform);
            }
            else
            {
                newItemBox.transform.SetParent(inventoryVertRows[1].transform);
            }
            //alternate between adding entries to the first and second row
            isFirstRow = !isFirstRow;

            var getObjects = newItemBox.GetComponent<GetObjectsResourceBox>();
            getObjects.GetTitleText().SetText(kvp.Key.Name);
            getObjects.GetCapacityText().SetText($"x{kvp.Value.ToString()}");
            getObjects.SetItemToolTip(kvp.Key.ItemDesc);
        }
    }

    public void PopulateResources()
    {
        int i = 0;
        foreach (var resource in InventoryWidget.resources)
        {
            var newBox = Instantiate(ResourceBox);
            if (i < 3)
            {
                newBox.transform.SetParent(RowOne.transform);
                i++;
            }
            else
            {
                newBox.transform.SetParent(RowTwo.transform);
                i++;
            }
            var getObjects = newBox.GetComponent<GetObjectsResourceBox>();
            getObjects.GetBGImage().color = new Color(resource.ResourceColor.r, resource.ResourceColor.g, resource.ResourceColor.b, 0.3f);
            getObjects.GetTitleText().SetText(resource.DisplayName.ToString() + "   (" + resource.Abreviation.ToString() + ")");
            getObjects.GetCapacityText().SetText("Capacity:\n" + (playerInventory.GetResourceAmount(resource) / 10) + "/10");

            foreach(var chunk in getObjects.GetChunkButtons())
            {
                chunk.GetComponent<Image>().color = resource.ResourceColor;
            }

            ResourceBoxes.Add(resource, getObjects);
        }
    }

    public void AddMassHUD()
    {
        var newBox = Instantiate(MassHUD);
        newBox.transform.SetParent(RowTwo.transform);
        var getObjects = newBox.GetComponent<GetObjectMassHUD>();

        massText = getObjects.GetMassText();
        speedModText = getObjects.GetSpeedText();

        UpdateMassHUD();
    }

    public void UpdateMassHUD()
    {
        massText.SetText($"{currentTotalMass}/500");
        speedModText.SetText($"% Speed");
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
