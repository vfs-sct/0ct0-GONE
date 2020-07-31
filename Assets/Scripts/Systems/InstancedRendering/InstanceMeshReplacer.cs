using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceMeshReplacer : MonoBehaviour
{

    [SerializeField] private InstancedRenderingModule IRenderingManager;

    [SerializeField] private Mesh LinkedMesh;

    [SerializeField] private MeshRenderer LinkedRenderer;

    private void Awake()
    {
        IRenderingManager.AddInstancedMesh(gameObject,InstancedRenderingModule.GenerateIMeshData(gameObject,LinkedMesh,LinkedRenderer));
        Destroy(LinkedMesh);
        Destroy(LinkedRenderer);
    }

}
