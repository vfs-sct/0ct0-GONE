﻿using System.Collections;
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
    [SerializeField] GameObject[] chunkButtons = null;

    private bool[] activeChunk = new bool[10];

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
