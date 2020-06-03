using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Crafting : MonoBehaviour
{
    [SerializeField] UIAwake UIRoot = null; 
    [SerializeField] GameObject HUDPrefab = null;
    [SerializeField] ScriptableObject[] T1Recipes;
    // Start is called before the first frame update

    [SerializeField] Button RecipeButton = null;
    
    //tier tabs
    [SerializeField] GameObject T1Panel = null;
    //[SerializeField] GameObject T2Panel = null;
    //[SerializeField] GameObject T3Panel = null;

    //panel that shows the recipes for each tab
    [SerializeField] Button T1TabButton = null;
    //[SerializeField] Button T2TabButton = null;
    //[SerializeField] Button T3TabButton = null;

    Dictionary<GameObject, Button> PanelToButton = new Dictionary<GameObject, Button>();

    private ResourceInventory playerInventory;
    void Awake()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();

        //associate the tab with its panel
        PanelToButton[T1Panel] = T1TabButton;

        //fill in the panel
        foreach(var recipe in T1Recipes)
        {
            AddNewButton(recipe.name, T1Panel.GetComponent<VerticalLayoutGroup>());
        }

        //set the default panel to active
        SwitchActiveTab(T1Panel);
    }

    //update numbers related to player inventory here
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public System.Action closeCallback;

    public void OnEsc(InputValue value)
    {
        Close();
    }

    public void Close()
    {
        SwitchViewTo(HUDPrefab);
    }

    public void SwitchActiveTab(GameObject active_panel)
    {
        foreach (var kvp in PanelToButton)
        {
            if (kvp.Key == active_panel)
            {
                kvp.Key.SetActive(true);
                kvp.Value.GetComponentInChildren<Button>().interactable = false;
            }
            else
            {
                kvp.Key.SetActive(false);
                kvp.Value.GetComponentInChildren<Button>().interactable = true;
            }
        }
    }

    public void AddNewButton(string buttonText, VerticalLayoutGroup contentGroup)
    {
        //Here we create an instance of the template button we serialized at the top and save it into a variable
        var newButton = Instantiate(RecipeButton);

        //Now using that variable we put the button into the navigation bar, which has settings in the editor
        //to automatically layout and space any buttons it contains
        newButton.transform.SetParent(contentGroup.transform);

        //Finally we get the text on the button from its children and set the text to the name of the codex entry
        newButton.GetComponentInChildren<TextMeshProUGUI>().SetText(buttonText);

        //Unlike the header, we must also add functionality to the button. My prefab is actually a game object, so we have to get the button from it 
        //first using GetComponent.
        //Buttons have a built-in "onClick" function and we'll add a listener to wait for the player to click the button to pop up our codex entry text.
        newButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            //Use the dictionary key to set the entry title
            //entryTitleText.SetText(buttonText);
            //Use the dictionary key to get the dictionary value, then set the body text
            //entryBodyText.SetText(dict[buttonText]);
        });
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
