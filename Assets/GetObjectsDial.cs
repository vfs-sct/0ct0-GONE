using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetObjectsDial : MonoBehaviour
{
    [SerializeField] Image bkImage = null;
    [SerializeField] Image fillImage = null;
    [SerializeField] TextMeshProUGUI text;

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
}
