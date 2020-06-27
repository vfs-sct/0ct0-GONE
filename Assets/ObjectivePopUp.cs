using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectivePopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText = null;
    [SerializeField] private TextMeshProUGUI objectiveText = null;
    public bool isFirst = true;

    private string newObjective = "New Objective: ";
    private string objectiveComplete = "Objective Complete: ";
    private string memoryReconstruction = "Reconstruction at ";
    private float reconstructionPercent;
    private string[] objectiveShort = new string[8]
    {
        "New Objective: Refuel at the station",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
    };

    private void OnEnable()
    {
        if(isFirst == false)
        {

        }
        else
        {
            isFirst = false;
        }
    }

    private void SetObjectiveText(bool isPreText, int index)
    {
        if(isPreText)
        {
            titleText.SetText(objectiveComplete);
            objectiveText.SetText(memoryReconstruction + ((index + 1) * 12.5).ToString() + "%");
        }
        else
        {
            titleText.SetText(newObjective);
            objectiveText.SetText(objectiveShort[index]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetObjectiveText(false, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
