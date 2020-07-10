using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private float MinDamageSpeed = 5;
    [SerializeField] private float DamageMultiplier = 1;
    [SerializeField] private Rigidbody _RigidBody;

    [SerializeField] private ResourceInventory PlayerResourceInventory;

    [SerializeField] private Resource HealthResource;

    void OnCollisionEnter(Collision collision)
    {
        float damageToApply = 0;
        float TargetMass = 1;
        Rigidbody targetRB = collision.collider.GetComponent<Rigidbody>();
        if (targetRB)
        {
            TargetMass = targetRB.mass;
        }
        
        float collisionSpeed = collision.relativeVelocity.magnitude;
       if (collisionSpeed >= MinDamageSpeed)
       {
           damageToApply = ((TargetMass)*(collisionSpeed-MinDamageSpeed)/100)*DamageMultiplier;
           PlayerResourceInventory.RemoveResource(HealthResource,damageToApply);
       }
    }

     
}
