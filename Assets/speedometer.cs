using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class speedometer : MonoBehaviour
{
    [SerializeField] MovementController movementController = null;
    [SerializeField] private TextMeshProUGUI speedText = null;
    [SerializeField] private Image barFill = null;

    private MovementComponent spaceMovement;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        spaceMovement = movementController.MovementModes[0];
    }

    // Update is called once per frame
    void Update()
    {
        speed = spaceMovement.GetSpeed();
        speedText.SetText($"Speed: {speed}km");
        barFill.fillAmount = speed / 100;
    }
}
