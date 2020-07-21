using UnityEngine;

public class RepairedInfo : MonoBehaviour
{
    [SerializeField] private RepairableComponent repairedInfo = null;

    public RepairableComponent GetRepairedComponent()
    {
        return repairedInfo;
    }
}
