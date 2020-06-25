//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryV2 : MonoBehaviour
{
    [SerializeField] UIAwake UIRoot = null;
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] GameObject ResourceBox = null;
    [SerializeField] HorizontalLayoutGroup RowOne = null;
    [SerializeField] HorizontalLayoutGroup RowTwo = null;

    [SerializeField] HUDInventoryWidget InventoryWidget = null;

    private InventoryController playerInventory;
 
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<InventoryController>();
        PopulateResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Cursor.visible = true;
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

            foreach(var chunk in getObjects.GetChunkImages())
            {
                chunk.color = resource.ResourceColor;
            }
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
