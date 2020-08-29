using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableGameFramework;


[CreateAssetMenu(menuName = "Systems/Pooling/Pooling Manager")]
public class PoolingModule : Module
{

    [SerializeField] private List<ObjectPool> ObjectPools = new List<ObjectPool>();

    [SerializeField] private Vector3 ZeroPos = new Vector3();

    public void InitializePools()
    {
        foreach (var pool in ObjectPools)
        {
            pool.InitializePool(ZeroPos);
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
        foreach (var pool in ObjectPools)
        {
            pool.Reset();
        }
    }
}
