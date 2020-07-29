using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Pooling/Pooling Manager")]
public class PoolingModule : Module
{

    [SerializeField] private List<ObjectPool> ObjectPools = new List<ObjectPool>();

    public void InitializePools()
    {
        foreach (var pool in ObjectPools)
        {
            pool.InitializePool();
        }
    }
    
    public override void Initialize()
    {
        Reset();
    }

    public ObjectPool GetPool(int PoolIndex) 
    {
        return ObjectPools[PoolIndex];
    }

    public override void Reset()
    {

    }
}
