using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI toolText = null;
    [SerializeField] public TextMeshProUGUI hotkeyText = null;
    [SerializeField] public Image toolIcon = null;

    public TextMeshProUGUI GetToolText()
    {
        return toolText;
    }

    public TextMeshProUGUI GetHotkeyText()
    {
        return hotkeyText;
    }

    public Image GetToolIcon()
    {
        return toolIcon;
    }

}
