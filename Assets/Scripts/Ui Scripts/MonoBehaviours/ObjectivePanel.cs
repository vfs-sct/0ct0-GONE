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
        //var newHeader = Instantiate(defaultObjectiveText);
        //gameHUD.objectiveText.Add(newHeader.GetComponentInChildren<TextMeshProUGUI>());
        //newHeader.transform.SetParent(contentGroup.transform);
    }

    public void UpdateObjective(int index, string newText)
    {
        gameHUD.objectiveText[index].SetText(newText);
    }

    public void AddObjective(string objectiveText)
    {
        //make sure objectives are on
        this.gameObject.SetActive(true);

        //create a new text box and add it
        var newHeader = Instantiate(defaultObjectiveText);
        gameHUD.objectiveText.Add(newHeader.GetComponentInChildren<TextMeshProUGUI>());
        newHeader.transform.SetParent(contentGroup.transform);
        
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
            Destroy(text);
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
