//Kristin Ruff-Frederickson | Copyright 2020©
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectivePanel : MonoBehaviour
{
    [SerializeField] Image backgroundPanel = null;
    [SerializeField] VerticalLayoutGroup contentGroup = null;
    [SerializeField] GameObject defaultObjectiveText = null;
    [SerializeField] public TextMeshProUGUI header = null;
    [SerializeField] public GameHUD gameHUD = null;

    // Start is called before the first frame update
    void Awake()
    {
        var newHeader = Instantiate(defaultObjectiveText);
        gameHUD.objectiveText.Add(newHeader.GetComponentInChildren<TextMeshProUGUI>());
        newHeader.transform.SetParent(contentGroup.transform);
    }

    public void AddObjective(string objectiveText)
    {
        this.gameObject.SetActive(true);
        var newHeader = Instantiate(defaultObjectiveText);
        gameHUD.objectiveText.Add(newHeader.GetComponentInChildren<TextMeshProUGUI>());
        newHeader.transform.SetParent(contentGroup.transform);
        var sizeDelta = backgroundPanel.GetComponent<RectTransform>().sizeDelta;
        sizeDelta.y += 65;
        backgroundPanel.GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    public void ClearObjectives()
    {

    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
}
