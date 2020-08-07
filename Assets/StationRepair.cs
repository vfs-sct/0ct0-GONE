using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StationRepair : MonoBehaviour
{
    [SerializeField] Player player = null;
    [SerializeField] InventoryController playerInventory = null;
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] GameObject HUDPrefab = null;
    [SerializeField] GameObject ComponentBox = null;

    [Header("Unrepaired Panel")]
    [SerializeField] GameObject repairPanel = null;
    [SerializeField] TextMeshProUGUI titleTxt = null;
    [SerializeField] Button repairButton = null;
    [SerializeField] GameObject popTextGO = null;
    [SerializeField] Image timerDial = null;
    [SerializeField] float buttonHoldTime = 0.5f;

    [SerializeField] HorizontalLayoutGroup[] rows = null;

    [Header("Prerequisite Section")]
    [SerializeField] private GameObject prerequisiteGO = null;
    [SerializeField] private TextMeshProUGUI repairObjectTxt = null;

    [Header("Completed Panel")]
    [SerializeField] private GameObject completePanel = null;
    [SerializeField] private TextMeshProUGUI completedTxt = null;

    [Header("Sound Related")]
    public bool isSoundPlayed;

    [Header("Do not touch")]
    public RepairableComponent currentSat = null;
    public RepairableInfo currentSatInfo = null;

    private float craftTimer = 4f;
    private bool isCrafting = false;

    private void Start()
    {
        //rows[1].gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isCrafting == true)
        {
            UpdateTimer();
        }
    }

    public void UpdateTimer()
    {
        //EVAN - a little timer dial pops up and might need a sound for when it's filling
        //if you click & hold something to craft you'll see it
        if (!isSoundPlayed)
        {
            AkSoundEngine.PostEvent("Octo_Repair_Start", gameObject);
            isSoundPlayed = true;
        }

        if (craftTimer != 0)
        {
            timerDial.gameObject.SetActive(true);
            craftTimer -= Time.unscaledDeltaTime;
            timerDial.fillAmount = (buttonHoldTime - craftTimer) / buttonHoldTime;
            if (craftTimer <= 0)
            {
                DoCraft();
                var popText = Instantiate(popTextGO);
                popText.GetComponentInChildren<TextMeshProUGUI>().SetText("Repaired");
                popText.transform.SetParent(repairButton.transform);
                popText.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                craftTimer = 0;
                timerDial.gameObject.SetActive(false);
                timerDial.fillAmount = 0f;
                isCrafting = false;
                ChooseDisplayPanel();
            }
        }
    }

    private void DoCraft()
    {
        //EVAN - if you have a bigger sounding "Complete!" sound it should go here,
        //since completing this repairs part of the main station and unlocks a codex

        AkSoundEngine.PostEvent("Crafting_Success", gameObject);

        currentSat.InstantComplete(player.gameObject);

        isSoundPlayed = false;
    }

    public void HoldTimer()
    {
        isCrafting = true;
        craftTimer = buttonHoldTime;
    }
    public void ReleaseTimer()
    {
        //Debug.Log("Release Timer");
        craftTimer = 0;
        timerDial.gameObject.SetActive(false);
        timerDial.fillAmount = 0f;
        isCrafting = false;
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

            //set the text for the item needed by the repair recipe
            var outputText = newUIBox.GetComponentsInChildren<TextMeshProUGUI>();
            outputText[0].SetText(component.item.Name);
            outputText[1].SetText(amountOwned.ToString());
            outputText[2].SetText($"/ {component.amount}");

            //set the icon for the item
            newUIBox.GetComponentInChildren<Image>().sprite = component.item.Icon;

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

    public void UpdateRepairButton()
    {
        if(!repairButton.gameObject.activeSelf)
        {
            return;
        }
        currentSat.SetupRepair(player.gameObject);
        if (!currentSat.CanRepair(player.gameObject))
        {
            repairButton.interactable = false;
            return;
        }
        repairButton.interactable = true;

        //button clicking setup
        EventTrigger trigger = repairButton.GetComponent<EventTrigger>();

        trigger.triggers.Clear();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventData) =>
        {
            if (!currentSat.CanRepair(player.gameObject))
            {
                return;
            }
            var pointerData = (PointerEventData)eventData;
            timerDial.transform.position = pointerData.position;
            HoldTimer();
        });

        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((eventData) =>
        {
            ReleaseTimer();
        });
        trigger.triggers.Add(entry);
    }

    private void OnEnable()
    {
        //EVAN 
        //sound for when you open the panel where you can inspect the large station objects
        //that can be repaired for narrative events - ie, Station Antenna, Station Solar Array, Station Power Storage etc
        Cursor.visible = true;
        AkSoundEngine.PostEvent("Complete_Objective", gameObject);
        titleTxt.SetText(currentSatInfo.DisplayName);
        ChooseDisplayPanel();
    }

    private void ChooseDisplayPanel()
    {
        if (currentSat.isRepaired)
        {
            if (!completePanel.activeSelf)
            {
                completePanel.SetActive(true);
                repairPanel.SetActive(false);
            }
            completedTxt.SetText($"{currentSatInfo.DisplayName} Repaired");
        }
        else
        {
            if (!repairPanel.activeSelf)
            {
                repairPanel.SetActive(true);
                completePanel.SetActive(false);
            }
            PopulateIngredients();
            PrerequisitesUpdate();
            UpdateRepairButton();
        }
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        ClearRows();
    }


    //used to get the current sat and then enabled the screen - dont use SetActive on stationrepair.gameobject directly
    public void OpenScreen(RepairableComponent newSat)
    {
        AkSoundEngine.PostEvent("Octo_Systems_Text", gameObject);
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
        AkSoundEngine.PostEvent("Octo_Systems_Text", gameObject);
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
