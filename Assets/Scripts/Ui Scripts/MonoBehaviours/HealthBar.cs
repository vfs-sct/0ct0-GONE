using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public UIAwake UIRoot = null;

    [SerializeField] public Image barFill = null;

    private HealthComponent playerHealth;
    private float maxHealth;

    private void Start()
    {
        playerHealth = UIRoot.GetPlayer().GetComponent<HealthComponent>();
        maxHealth = playerHealth.MaxHealth;
    }

    private void Update()
    {
        float charStat = playerHealth.Health;

        // set fill to health percentage
        //needs a way to get the max value from the resource instead of hardcoding division number
        barFill.fillAmount = charStat / maxHealth;
    }
}
