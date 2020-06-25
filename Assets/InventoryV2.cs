//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;

public class InventoryV2 : MonoBehaviour
{
    [SerializeField] UIAwake UIRoot = null;
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] GameObject ResourceBox = null;
    [SerializeField] HorizontalLayoutGroup RowOne = null;
    [SerializeField] HorizontalLayoutGroup RowTwo = null;

    [SerializeField] HUDInventoryWidget InventoryWidget = null;

    private InventoryController playerInventory;
    private Dictionary<Resource, GetObjectsResourceBox> ResourceBoxes = new Dictionary<Resource, GetObjectsResourceBox>();
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
            kvp.Value.GetCapacityText().SetText("Capacity:\n" + (playerInventory.GetFillAmount(kvp.Key) / 10) + "/10");
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
                button.SetActive(false);
            }

            var bucket = playerInventory.GetResourceBucket(kvp.Key);
            int i = 0;
            //Debug.Log(bucket.Bucket);
            //get all the items in the bucket
            foreach(var item in bucket.Bucket)
            {
                Debug.Log("HEY" + item.Size / 10);
                //figure out if the item takes more than 1 slot
                for(float j = item.Size/10;  j > 0; j--)
                {
                    //assign the appropriate number of slots for that item
                    for (int k = 0; k < isActive.Length; k++)
                    {
                        kvp.Value.SetChunkBool(k, true);
                        kvp.Value.GetChunkButtons()[k].SetActive(true);
                        isActive[k] = true;
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
            getObjects.GetCapacityText().SetText("Capacity:\n" + (playerInventory.GetFillAmount(resource) / 10) + "/10");

            foreach(var chunk in getObjects.GetChunkButtons())
            {
                chunk.GetComponent<Image>().color = resource.ResourceColor;
            }

            ResourceBoxes.Add(resource, getObjects);
        }
    }

    //public Button AddNewButton(string buttonText, VerticalLayoutGroup contentGroup)
    //{
    //    var newButton = Instantiate(RecipeButton);

    //    newButton.transform.SetParent(contentGroup.transform);

    //    newButton.GetComponentInChildren<TextMeshProUGUI>().SetText(buttonText);

    //    return newButton;
    //}

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
