using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
{
    [SerializeField] private FillBar fillbar = null;
    [SerializeField] private Image[] barImages = null;

    public void Upgrade(float UpgradeAmount)
    {
        foreach(var image in barImages)
        {
            var anchoredPos = image.GetComponent<RectTransform>().anchoredPosition;
            anchoredPos.x -= UpgradeAmount / 2;
            image.GetComponent<RectTransform>().anchoredPosition = anchoredPos;

            var sizeDelta = image.GetComponent<RectTransform>().sizeDelta;
            sizeDelta.x += UpgradeAmount;
            image.GetComponent<RectTransform>().sizeDelta = sizeDelta;
        }

        SetNewMax(UpgradeAmount);
    }

    public void SetNewMax(float UpgradeAmount)
    {
        Debug.Log($"Old max: {fillbar.fuel.GetMaximum()}");
        fillbar.fuel.SetMaximum(fillbar.fuel.GetMaximum() + UpgradeAmount);
        Debug.Log($"New max: {fillbar.fuel.GetMaximum()}");
    }
}
