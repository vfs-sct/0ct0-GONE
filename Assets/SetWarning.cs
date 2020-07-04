using UnityEngine;
using TMPro;

public class SetWarning : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI warningText = null;

    public void MakeActive(string warningMsg)
    {
        warningText.SetText(warningMsg);
        gameObject.SetActive(true);
    }

}
