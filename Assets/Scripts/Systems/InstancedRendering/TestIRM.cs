using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIRM : MonoBehaviour
{

    [SerializeField] private InstancedRenderingModule IRenderModule;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        IRenderModule.Update();
    }
}
