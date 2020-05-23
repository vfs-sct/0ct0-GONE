//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Confirmation : MonoBehaviour
{
    [SerializeField] public TMP_Text titleText;
    [SerializeField] public TMP_Text bodyText;

    public System.Action clickConfirmCallback;

    public void OnEsc(InputValue value)
    {
        gameObject.SetActive(false);
    }

    public void OnClickConfirm()
    {
        clickConfirmCallback();

        clickConfirmCallback = null;
    }

    public void OnClickCancel()
    {
        gameObject.SetActive(false);

        clickConfirmCallback = null;
    }

    public void SwitchViewTo(GameObject newPanel)
    {
        newPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
