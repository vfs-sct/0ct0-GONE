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
    [SerializeField] HorizontalLayoutGroup RowOne = null;
    [SerializeField] HorizontalLayoutGroup RowTwo = null;
    //place item in the item inventory screen
    [SerializeField] GameObject defaultInventoryItem = null;

    [SerializeField] HUDInventoryWidget InventoryWidget = null;
    [SerializeField] EventSystem eventSystem = null;

    [SerializeField] Button[] tabs;
    [SerializeField] GameObject[] tabContent;

    [SerializeField] VerticalLayoutGroup[] inventoryVertRows = null;

    private InventoryController playerInventory;

    //association between a resource box and the resource it's displaying
    private Dictionary<Resource, GetObjectsResourceBox> ResourceBoxes = new Dictionary<Resource, GetObjectsResourceBox>();

    //save an association between the chunk and the specific item that's filling it in - multiple chunks can use the same item
    private Dictionary<Button, Item> ItemButtonAssociation = new Dictionary<Button, Item>();

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

            //if tab is clicked, show the tabs contents and make the tab non-interactable
            tabs[i].onClick.AddListener(() =>
            {
                SwitchTab(i);
            });
        }
    }

    private void SwitchTab(int index)
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
        playerInventory = UIRoot.GetPlayer().GetComponent<InventoryController>();
        PopulateResources();
        UpdateAllChunks();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var kvp in ResourceBoxes)
        {
            kvp.Value.GetCapacityText().SetText("Capacity:\n" + (playerInventory.GetResourceAmount(kvp.Key) / 10) + "/10");
        }
    }

    private void OnEnable()
    {
        Cursor.visible = true;
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
                                kvp.Value.SetTooltip(k, item.Key.Name, chunkSize.ToString() + " Slots");
                                kvp.Value.GetChunkButtons()[k].GetComponent<Button>().onClick.AddListener(() =>
                                {
                                //Debug.Log("CHUNK CLICKED!");
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

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
