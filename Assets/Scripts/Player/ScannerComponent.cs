using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerComponent : MonoBehaviour
{
   


    [SerializeField] private AK.Wwise.Event PlayStartupSound;
    [SerializeField] private AK.Wwise.Event PlayDeactivateSound;
    
    [SerializeField] private InstancedRenderingModule IRenderModule;

    private bool IsScanning = false;

    public void DoScan()
    {
        PlayStartupSound.Post(gameObject);
        IRenderModule.IsHighlighted = true;
        StartCoroutine(FinishScan());
    }

    IEnumerator FinishScan()
    {
        yield return new WaitForSeconds(5f);;
        PlayDeactivateSound.Post(gameObject);
        IRenderModule.IsHighlighted = false;
        IsScanning = false;
    }

}
