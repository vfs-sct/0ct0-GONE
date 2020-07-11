using UnityEngine;
using TMPro;

//single instance of set warning is access by all warning types
public class SetWarning : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI warningText = null;

    public void MakeActive(string warningMsg)
    {
        warningText.SetText(warningMsg);
        gameObject.SetActive(true);
    }

}
