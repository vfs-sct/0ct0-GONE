using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectivePanel : MonoBehaviour
{
    [SerializeField] VerticalLayoutGroup contentGroup = null;
    [SerializeField] GameObject defaultObjectiveText = null;

    public int currentEvent = 0;

    private string[][] events = new string[][]
    {
        //OBJECTIVE ONE
        new string[]
        {
            "- Repair the broken space ship",
        },

        //OBJECTIVE TWO
        new string[]
        {
            "Example",
            "Example",
            "Example",
        },
    };

    // Start is called before the first frame update
    void Awake()
    {
        LoadObjectives(events[currentEvent]);
    }

    private void LoadObjectives(string[] narrativeSet)
    {
        //TODO - code to delete objectives leftover in the display before putting the new ones in
        //foreach()
        //{
        //}

        foreach (var objective in narrativeSet)
        {
            var newHeader = Instantiate(defaultObjectiveText);

            newHeader.transform.SetParent(contentGroup.transform);
            newHeader.GetComponentInChildren<TextMeshProUGUI>().SetText(objective);
        }
    }
}
