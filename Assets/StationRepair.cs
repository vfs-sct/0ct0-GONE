using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StationRepair : MonoBehaviour
{
    [SerializeField] Player player = null;
    [SerializeField] InventoryController playerInventory = null;
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] GameObject HUDPrefab = null;
    [SerializeField] GameObject ComponentBox = null;
    [SerializeField] TextMeshProUGUI titleTxt = null;
    [SerializeField] Button repairButton = null;

    [SerializeField] HorizontalLayoutGroup[] rows = null;

    [Header("Prerequisite Section")]
    [SerializeField] private GameObject prerequisiteGO = null;
    [SerializeField] private TextMeshProUGUI repairObjectTxt = null;

    [Header("Do not touch")]
    public RepairableComponent currentSat = null;
    public RepairableInfo currentSatInfo = null;

    private void Start()
    {
        rows[1].gameObject.SetActive(false);
    }

    public void ClearRows()
    {
        for(int i = 0; i < rows.Length; i++)
        {
            int childCount = rows[i].transform.childCount;

            //return early if there's nothing to remove
            if(childCount == 0)
            {
                return;
            }

            //remove previous components
            for (int j = 0; j < childCount; j++)
            {
                Destroy(rows[i].transform.GetChild(j).gameObject);
            }
        }
    }

    public void PrerequisitesUpdate()
    {
        var previousRepair = currentSat.previousRepair;

        if (currentSat.previousRepair == null || previousRepair.isRepaired)
        {
            //enable repairing
            repairButton.gameObject.SetActive(true);
            repairButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Repair {currentSatInfo.AbrName}");

            prerequisiteGO.SetActive(false);
            return;
        }
        else
        {
            //turn on the prerequisite info box
            prerequisiteGO.SetActive(true);

            //set the info
            if (!previousRepair.isRepaired)
            {
                //prevent repairing
                repairButton.gameObject.SetActive(false);

                repairObjectTxt.SetText(previousRepair.gameObject.GetComponentInParent<RepairableInfo>().DisplayName);
            }
        }
    }

    public void PopulateIngredients()
    {
        int componentCount = 0;
        foreach(var component in currentSat.RequiredComponents)
        {
            var newUIBox = Instantiate(ComponentBox);
            if(componentCount < 2)
            {
                newUIBox.transform.SetParent(rows[0].transform);
            }
            else
            {
                newUIBox.transform.SetParent(rows[1].transform);
            }

            int amountOwned = playerInventory.GetItemAmount(component.item);
            if (amountOwned < 0) amountOwned = 0;

            var outputText = newUIBox.GetComponentsInChildren<TextMeshProUGUI>();
            outputText[0].SetText(component.item.Name);
            outputText[1].SetText(amountOwned.ToString());
            outputText[2].SetText($"/ {component.amount}");

            componentCount++;
        }
        if(componentCount <= 2)
        {
            rows[1].gameObject.SetActive(false);
        }
        else
        {
            rows[1].gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        titleTxt.SetText(currentSatInfo.DisplayName);
        PopulateIngredients();
        PrerequisitesUpdate();
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        ClearRows();
    }


    //used to get the current sat and then enabled the screen - dont use SetActive on stationrepair.gameobject directly
    public void OpenScreen(RepairableComponent newSat)
    {
        currentSat = newSat;
        currentSatInfo = currentSat.gameObject.GetComponentInParent<RepairableInfo>();
        this.gameObject.SetActive(true);
    }

    //crafting screen can either be closed with ESC or the hotkey to open it (or clicking the close button on the panel)
    public void OnEsc(InputValue value)
    {
        Close();
    }
    public void OnRepairScreenHotkey(InputValue value)
    {
        Close();
    }

    public void Close()
    {
        GameManager.UnPause();
        Debug.Log("Unpaused");
        SwitchViewTo(HUDPrefab);
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
