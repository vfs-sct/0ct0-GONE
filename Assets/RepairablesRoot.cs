using UnityEngine;

public class RepairablesRoot : MonoBehaviour
{
    [SerializeField] EventModule EventModule = null;

    [SerializeField] private GameObject[] repairables = null;

    public GameObject GetRepairable(int index)
    {
        return repairables[index];
    }
    //[SerializeField] GameObject Antenna = null;
    //[SerializeField] GameObject SolarPanel = null;
    //[SerializeField] GameObject Battery = null;
    //[SerializeField] GameObject SignalProcessingUnit = null;
    // Start is called before the first frame update
    void Start()
    {
        EventModule.SetRepairableRoot(this);
    }
}
