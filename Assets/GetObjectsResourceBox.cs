using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetObjectsResourceBox : MonoBehaviour
{
    [SerializeField] GameObject itemHoverGO = null;
    [SerializeField] TextMeshProUGUI hoverTooltip = null;
    [SerializeField] Image bgImage = null;
    [SerializeField] TextMeshProUGUI titleText = null;
    [SerializeField] TextMeshProUGUI capacityText = null;
    [Header("Must Be Size 10:")]
    [SerializeField] GameObject[] chunkButtons = null;

    private bool[] activeChunk = new bool[10];

    //used for item inventory items
    public void SetItemToolTip(string tooltipText)
    {
        hoverTooltip.SetText(tooltipText);
    }
    //used for item inventory items
    public void ShowItemTooltip()
    {
        itemHoverGO.SetActive(true);
    }
    //used for item inventory items
    public void HideItemTooltip()
    {
        itemHoverGO.SetActive(false);
    }

    //used for chunk inventory
    public void SetChunkTooltip(int index, string title, string amount)
    {
        chunkButtons[index].GetComponent<ChunkHoverInfo>().titleText = title;
        chunkButtons[index].GetComponent<ChunkHoverInfo>().amountText = amount;
    }

    public void SetChunkBool(int index, bool state)
    {
        activeChunk[index] = state;
    }

    public Image GetBGImage()
    {
        return bgImage;
    }
    public GameObject[] GetChunkButtons()
    {
        return chunkButtons;
    }

    public TextMeshProUGUI GetTitleText()
    {
        return titleText;
    }

    public TextMeshProUGUI GetCapacityText()
    {
        return capacityText;
    }
}
