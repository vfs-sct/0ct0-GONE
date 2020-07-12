using UnityEngine;
using TMPro;

public class GetObjectMassHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI massHeldTxt = null;
    [SerializeField] private TextMeshProUGUI speedModifierTxt = null;
    
    public TextMeshProUGUI GetMassText()
    {
        return massHeldTxt;
    }

    public TextMeshProUGUI GetSpeedText()
    {
        return speedModifierTxt;
    }
}
