using UnityEngine;
using UnityEngine.UI;

public class ShipHealthBar : MonoBehaviour
{
    [SerializeField] private Image barFill = null;
    [SerializeField] private Image barOverlay = null;
    private float maxHealth = 100;

    public void SetMaxHealth(float newMax)
    {
        maxHealth = newMax;
    }

    public void SetFill(float newHealth)
    {
        var newAmount = newHealth / maxHealth;
        barFill.fillAmount = newAmount;
        barOverlay.fillAmount = newAmount;
    }
}
