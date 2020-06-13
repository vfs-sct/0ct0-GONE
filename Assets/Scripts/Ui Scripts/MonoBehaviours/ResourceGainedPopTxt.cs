using UnityEngine;
using TMPro;

public class ResourceGainedPopTxt : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI popText;
    private Color lerpToColor;
    private float lerpTime = 0.2f;
    private float upSpeed = 100f;

    private void Awake()
    {
        lerpToColor = new Color(popText.color.r, popText.color.g, popText.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (popText.color.a > 0.01f)
        {
            popText.color = Color.Lerp(popText.color, lerpToColor, Time.unscaledDeltaTime * 1f / lerpTime);

            popText.transform.position = popText.transform.position + Vector3.up * Time.unscaledDeltaTime * upSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
