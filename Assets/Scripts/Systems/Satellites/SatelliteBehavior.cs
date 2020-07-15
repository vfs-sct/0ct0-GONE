using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteBehavior : MonoBehaviour
{

    public virtual bool PlacementConditionCheck(ToolController Owner)
    {
        return true;
    }

    public virtual void WhilePlacing(ToolController Owner){}

    public virtual void OnPlacePreview(ToolController Owner){}

}
