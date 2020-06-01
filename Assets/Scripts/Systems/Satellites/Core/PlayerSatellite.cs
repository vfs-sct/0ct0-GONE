using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "Systems/Satellite/New Satellite")]
public abstract class PlayerSatellite : ScriptableObject
{
    [SerializeField] protected GameObject _PlacementPrefab;
    public GameObject PlacementPrefab {get =>_PlacementPrefab;}

    [SerializeField] protected GameObject _CarryingPrefab;
    public GameObject CarryingPrefab {get =>_CarryingPrefab;}

    [SerializeField] protected float _Mass = 10f;
    public float Mass{get => _Mass;}

    [SerializeField] protected bool _CanUpdate = false;
    public bool CanUpdate{get => _CanUpdate;}
    [SerializeField] protected bool _CanInteract = false;
    public bool CanInteract{get => _CanInteract;}

    [SerializeField] protected bool _CanPickup = true;
    public bool CanPickup{get => _CanPickup;}

    public abstract void UpdateSat(PlayerSatelliteBehavior Parent);

    public virtual bool Interact(PlayerSatelliteBehavior Parent)
    {
        return true;
    }

    protected virtual void InteractEffect(PlayerSatellite Parent)
    {
    }
    public abstract void Init(PlayerSatelliteBehavior Parent);
    public abstract bool PlacementCondition(PlayerSatelliteBehavior Parent);
    public abstract GameObject Place(PlayerSatelliteHolder Parent);
    public abstract void Pickup(PlayerSatelliteBehavior Parent,GameObject NewSat);
}
