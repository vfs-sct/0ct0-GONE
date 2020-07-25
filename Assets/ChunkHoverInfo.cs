using UnityEngine;
using TMPro;

public class ChunkHoverInfo : MonoBehaviour
{
    [SerializeField] private GameObject GO = null;
    [SerializeField] private TextMeshProUGUI title = null;
    [SerializeField] private TextMeshProUGUI slotAmount = null;

    public string titleText;
    public string amountText;

    public void OnHover()
    {
        title.SetText(titleText);
        slotAmount.SetText(amountText);
        GO.SetActive(true);
        AkSoundEngine.PostEvent("Octo_Systems_Text", GO);
    }

    public void OnHoverEnd()
    {
        GO.SetActive(false);
    }

    private void OnDisable()
    {
        GO.SetActive(false);
    }
}
