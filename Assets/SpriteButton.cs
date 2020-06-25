using UnityEngine;
using UnityEngine.UI;

public class SpriteButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.01f;
    }
}