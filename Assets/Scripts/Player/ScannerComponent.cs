using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerComponent : MonoBehaviour
{
    struct SalvageRenderData
    {
        public Material OriginalMaterial;

        public Color OriginalColor;
        public MeshRenderer Renderer;

        public Salvagable SalvageData;

        public SalvageRenderData(Material M,MeshRenderer R,Color C,Salvagable S)
        {
            OriginalMaterial = M;
            Renderer = R;
            SalvageData = S;
            OriginalColor = C;
        }

    }


    [SerializeField] private AK.Wwise.Event PlayStartupSound;
    [SerializeField] private AK.Wwise.Event PlayDeactivateSound;
    
    [SerializeField] private float ScanRange = 500;
    [SerializeField] private LayerMask Mask;

    [SerializeField] private Material HighlightMaterial;


    private Dictionary<GameObject,SalvageRenderData> ScanResults = new Dictionary<GameObject,SalvageRenderData>();
    private List<GameObject> ScanKeys = new List<GameObject>();


    private void UpdateSalvageData()
    {
        ScanResults.Clear();
        ScanKeys.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position,ScanRange,Mask);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            
            Salvagable SalvageComp = hitColliders[i].GetComponentInChildren<Salvagable>();

            //don't add to salvage data if the game object is already there or if it isn't salvagable
            if (SalvageComp != null && !ScanResults.ContainsKey(hitColliders[i].gameObject))
            {
                MeshRenderer TempRender = hitColliders[i].GetComponentInChildren<MeshRenderer>();
                ScanResults.Add(hitColliders[i].gameObject,new SalvageRenderData(TempRender.material,TempRender,TempRender.material.color,SalvageComp));
                ScanKeys.Add(hitColliders[i].gameObject);
            }
        }
    }

    private void ShowHighlights()
    {
        for (int i = 0; i < ScanKeys.Count; i++)
        {
            ScanResults[ScanKeys[i]].Renderer.material = ScanResults[ScanKeys[i]].SalvageData.SalvageItem.ResourceType.ResourceHighlight;
            ScanResults[ScanKeys[i]].Renderer.material.color = ScanResults[ScanKeys[i]].SalvageData.SalvageItem.ResourceType.ResourceColor;
        }
    }
    
    private void RemoveHighlights()
    {
        for (int i = 0; i < ScanKeys.Count; i++)
        {
            if (ScanResults[ScanKeys[i]].Renderer != null)
            {
                ScanResults[ScanKeys[i]].Renderer.material = ScanResults[ScanKeys[i]].OriginalMaterial;
                ScanResults[ScanKeys[i]].Renderer.material.color = ScanResults[ScanKeys[i]].OriginalColor;
            }
        }
    }


    public void DoScan()
    {
        PlayStartupSound.Post(gameObject);
        UpdateSalvageData();
        ShowHighlights();
        StartCoroutine(FinishScan());
    }

    IEnumerator FinishScan()
    {
        yield return new WaitForSeconds(5f);;
        PlayDeactivateSound.Post(gameObject);
        RemoveHighlights();
    }

}
