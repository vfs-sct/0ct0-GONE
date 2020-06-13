//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectivePanel : MonoBehaviour
{
    [SerializeField] VerticalLayoutGroup contentGroup = null;
    [SerializeField] GameObject defaultObjectiveText = null;
    [SerializeField] public TextMeshProUGUI header = null;
    [SerializeField] public GameHUD gameHUD = null;

    // Start is called before the first frame update
    void Awake()
    {
        var newHeader = Instantiate(defaultObjectiveText);
        gameHUD.objectiveText = newHeader.GetComponentInChildren<TextMeshProUGUI>();
        newHeader.transform.SetParent(contentGroup.transform);
    }
}
