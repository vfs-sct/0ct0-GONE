using UnityEngine;


public class CommunicationZone : MonoBehaviour
{
    [SerializeField] private EventModule EventModule = null;
    [SerializeField] private CommunicationModule CommunicationManager = null;

    [SerializeField] private float _Radius = 5000;
    public float Radius{get=>_Radius;}

    private int ZoneIndex = -1;

    void Start()
    {
        ZoneIndex = CommunicationManager.AddZone(this);
        EventModule.SetCommZone(this);
    }

    public void AddRange(float addRange)
    {
        _Radius += addRange;
        Debug.Log("New comm range: {_Radius}");
    }
}
