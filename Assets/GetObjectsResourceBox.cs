using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetObjectsResourceBox : MonoBehaviour
{
    [SerializeField] Image bgImage = null;
    [SerializeField] TextMeshProUGUI titleText = null;
    [SerializeField] TextMeshProUGUI capacityText = null;
    [Header("Must Be Size 10:")]
    [SerializeField] Image[] chunkImages = null;

    public Image GetBGImage()
    {
        return bgImage;
    }
    public Image[] GetChunkImages()
    {
        return chunkImages;
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
