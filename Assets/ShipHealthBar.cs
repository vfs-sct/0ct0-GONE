using UnityEngine;
using UnityEngine.UI;

public class ShipHealthBar : MonoBehaviour
{
    [SerializeField] private Image barFill = null;
    private float maxHealth = 100;

    public void SetMaxHealth(float newMax)
    {
        maxHealth = newMax;
    }

    public void SetFill(float newHealth)
    {
        barFill.fillAmount = newHealth / maxHealth;
    }
}
