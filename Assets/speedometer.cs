using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class speedometer : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody = null;
    [SerializeField] private string noun = "Velocity";
    [SerializeField] private TextMeshProUGUI speedText = null;
    [SerializeField] private Image barFill = null;

    private float speed;

    // Update is called once per frame
    void Update()
    {
        speed = rigidBody.velocity.magnitude;
        if (speed < 10)
        {
            speedText.SetText($"{noun}: {Math.Round(speed, 1)}m/s");
        }
        else if(speed > 100)
        {
            speedText.SetText($"{noun}: FAST!!");
        }
        else
        {
            speedText.SetText($"{noun}: {Math.Round(speed, 0)}m/s");
        }
        barFill.fillAmount = speed / 30;
    }
}
