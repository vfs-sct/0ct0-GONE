using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Events/Repair Object Event")]
public class RepairObjectEvent : Event
{



    [SerializeField] private string Adjective = "Repair ";



    private RepairableComponent LinkedComponent;

    public override bool Condition(GameObject target)
    {
        return (LinkedComponent.RepairPercentage >= 1);
    }

    public void RegisterNewComponent(RepairableComponent newComp)
    {
        LinkedComponent = newComp;
    }

    protected override void Effect(GameObject target)
    {
        LinkedComponent = null;
    }
}
