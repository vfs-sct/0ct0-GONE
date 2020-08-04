using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceMeshReplacer : MonoBehaviour
{

    [SerializeField] private InstancedRenderingModule IRenderingManager;

    [SerializeField] private MeshFilter LinkedMesh;

    [SerializeField] private MeshRenderer LinkedRenderer;

    private void Awake()
    {
        IRenderingManager.AddInstancedMesh(transform.parent.gameObject,InstancedRenderingModule.GenerateIMeshData(gameObject,LinkedMesh.sharedMesh,LinkedRenderer));
        Destroy(LinkedMesh);
        Destroy(LinkedRenderer);
        Destroy(this); //remove this script
    }

}
