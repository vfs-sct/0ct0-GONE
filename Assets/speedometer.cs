using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class speedometer : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody = null;
    [SerializeField] private TextMeshProUGUI speedText = null;
    [SerializeField] private Image barFill = null;
    private float speed;
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        speed = rigidBody.velocity.magnitude;
        if (speed < 10)
        {
            speedText.SetText($"Speed: {Math.Round(speed, 1)}m/s");
        }
        else if(speed > 100)
        {
            speedText.SetText($"Speed: FAST!!");
        }
        else
        {
            speedText.SetText($"Speed: {Math.Round(speed, 0)}m/s");
        }
        barFill.fillAmount = speed / 30;
    }
}
