using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] UIAwake UIRoot = null;
    [SerializeField] VerticalLayoutGroup vLayoutGroup = null;
    [SerializeField] GameObject defaultText = null;
    [SerializeField] GameObject defaultButton = null;

    [SerializeField] Resource[] resourceList = null;
    [SerializeField] TMP_Dropdown DropDown = null;


    [SerializeField] List<string> resourceNamesList = new List<string>();

    private ResourceInventory playerInventory;

    private Resource fuel;

    private GameObject fuelStat;

    void Awake()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();

        AddNewText("Select resource from dropdown to add 50 of that resource");

        var resourceDropDown = Instantiate(DropDown);
        resourceDropDown.transform.SetParent(vLayoutGroup.transform);

        foreach (var resource in resourceList)
        {
            resourceNamesList.Add(resource.DisplayName);

            if (resource.DisplayName == "Thruster Fuel")
            {
                fuel = resource;
            }
        }
        
        resourceDropDown.options.Clear();

        resourceDropDown.AddOptions(resourceNamesList);
        resourceDropDown.onValueChanged.AddListener(evt =>
        {
            playerInventory.TryAdd(resourceList[resourceDropDown.value], 50);
        });

        AddNewButton("Kill Octo", () => { playerInventory.SetResource(fuel, 0f); });

        fuelStat = AddNewText("Fuel Stat: " + playerInventory.GetResource(fuel));
    }

    public void KillOcto()
    {
        if (fuel == null)
        {
            return;
        }

        playerInventory.SetResource(fuel, 0f);
    }

    GameObject AddNewText(string headerText)
    {
        var newHeader = Instantiate(defaultText);
        newHeader.transform.SetParent(vLayoutGroup.transform);
        newHeader.GetComponentInChildren<TextMeshProUGUI>().SetText(headerText);

        return newHeader;
    }

    //add a button with a label and onclick function
    public void AddNewButton(string buttonText, System.Action buttonFunction)
    {
        var newButton = Instantiate(defaultButton);

        newButton.transform.SetParent(vLayoutGroup.transform);
        newButton.GetComponentInChildren<TextMeshProUGUI>().SetText(buttonText);

        newButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            //pass in the function you want the button to do when clicked
            buttonFunction();
        });
    }

    private void Update()
    {
        fuelStat.GetComponent<TextMeshProUGUI>().SetText("Fuel Stat: " + playerInventory.GetResource(fuel));
    }

    //tilde to close
    public void OnDebug(InputValue value)
    {
        gameObject.SetActive(false);
    }
}
