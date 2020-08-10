using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Throttle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI throttleText = null;
    [SerializeField] private Image barFill = null;
    // Start is called before the first frame update
    private float maxVelocity;

    public void SetMaxVelocity(float maxVel)
    {
        maxVelocity = maxVel;
        //Debug.Log($"Starting MaxVel is {maxVelocity}");
    }

    public void UpdateUI(float throttle, float newVelMax)
    {
        throttleText.SetText($"Throttle: {throttle}%");
        barFill.fillAmount = 1 - (newVelMax / maxVelocity);
        //Debug.Log($"Max Velocity is {maxVelocity}//Throttled Max is {newVelMax}/n/n{newVelMax / maxVelocity}");
    }
}
