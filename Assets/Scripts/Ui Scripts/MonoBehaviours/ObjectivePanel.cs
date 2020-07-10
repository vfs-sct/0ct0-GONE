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

    public void UpdateObjective(int index, string newText)
    {
        gameHUD.objectiveText[index].SetText(newText);
    }

    public void AddObjective(string objectiveText)
    {
        //make sure objectives are on
        this.gameObject.SetActive(true);

        //create a new text box and add it to the objective panel
        var newHeader = Instantiate(defaultObjectiveText);
        newHeader.transform.SetParent(contentGroup.transform);

        //set and save the text
        var headerText = newHeader.GetComponentInChildren<TextMeshProUGUI>();
        gameHUD.objectiveText.Add(headerText);
        headerText.SetText(objectiveText);
        
        //adjust the background to fit the new number of text boxes
        var sizeDelta = backgroundPanel.GetComponent<RectTransform>().sizeDelta;
        sizeDelta.y += 65;
        backgroundPanel.GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    public void ClearObjectives()
    {
        //remove all of the textboxes
        foreach(var text in gameHUD.objectiveText)
        {
            Destroy(text.gameObject);
        }
        gameHUD.objectiveText.Clear();

        //set the size back to default
        var sizeDelta = backgroundPanel.GetComponent<RectTransform>().sizeDelta;
        sizeDelta.y = 0;
        backgroundPanel.GetComponent<RectTransform>().sizeDelta = sizeDelta;

        //hide the panel
        HidePanel();
    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
}
