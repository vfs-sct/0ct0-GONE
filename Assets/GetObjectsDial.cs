using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetObjectsDial : MonoBehaviour
{
    [SerializeField] Image bkImage = null;
    [SerializeField] Image fillImage = null;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI capacityText = null;

    public Image GetBKImage()
    {
        return bkImage;
    }

    public Image GetFillImage()
    {
        return fillImage;
    }

    public TextMeshProUGUI GetText()
    {
        return text;
    }

    public TextMeshProUGUI GetCapacityText()
    {
        return capacityText;
    }
}
