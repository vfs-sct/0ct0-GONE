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

    private const float DamageModifier = 0.01f;

    void OnCollisionEnter(Collision collision)
    {
        float damageToApply = 0;
        
        float collisionSpeed = collision.relativeVelocity.magnitude;
        float collisionMass = Mathf.Infinity;
        
        if (collision.rigidbody != null)
        {
            collisionMass = collision.rigidbody.mass;
        }

        //gets the lowest mass in the collision
        if (collisionMass > _RigidBody.mass)
        {
            collisionMass =  _RigidBody.mass;
        }

       if (collisionSpeed >= MinDamageSpeed)
       {
           damageToApply = collision.relativeVelocity.magnitude * (collisionMass) * DamageModifier * DamageMultiplier;
           PlayerResourceInventory.RemoveResource(HealthResource,damageToApply);
       }
    }

     
}
