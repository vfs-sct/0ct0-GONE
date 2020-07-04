using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SatelliteBehavior : MonoBehaviour
{

    public abstract bool PlacementConditionCheck(ToolController Owner);

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
