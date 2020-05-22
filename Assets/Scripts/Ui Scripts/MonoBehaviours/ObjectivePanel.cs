using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectivePanel : MonoBehaviour
{
    [SerializeField] VerticalLayoutGroup vLayoutGroup = null;
    [SerializeField] GameObject defaultObjectiveText = null;

    private int currentEvent = 0;

    Dictionary<string, string> narrativeObjectives = new Dictionary<string, string>
    {
        {"Objective01", "Repair the destroyed ship"},
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
