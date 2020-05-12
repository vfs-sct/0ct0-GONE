using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/CommRelayModule")]
public class CommunicationModule : Module
{

    public delegate void CommRelayEvent(CommunicationZone ActiveZone);

    private struct CommRelayModule
    {
        public CommunicationZone LinkedObject;
        public float Radius;
        public bool IsActive;

        public CommRelayModule(CommunicationZone G,float R, bool A)
        {
            LinkedObject = G;
            Radius = R;
            IsActive = A;
        }
    }

    private CommRelayEvent OnLoseConnection = null;
    private GameObject PlayerObject = null;
    private bool PlayerInRange = true;
    private CommunicationZone NearestRelay = null;
    private float NearestRelayDistance = 999999;

    private List<CommRelayModule> Zones = new List<CommRelayModule>();

    public override void Initialize()
    {

    }

    public override void Start()
    {
        Debug.Assert(PlayerObject != null); //at this point the gamestate should have assigned these values
        RunUpdate = true;
    }

    public int AddZone(CommunicationZone NewZone)
    {
        Zones.Add(new CommRelayModule(NewZone,NewZone.Radius,NewZone.enabled));
        return Zones.Count-1;
    }

    public void AddLeaveZoneEvent(CommRelayEvent NewDelegate)
    {
        OnLoseConnection += NewDelegate;
    }

    public void RemoveZone(int Index)
    {
        Zones.RemoveAt(Index);
    }
    public void SetPlayer(GameObject PlayerObj)
    {
        PlayerObject = PlayerObj;
    }




    private void CommunicationUpdate()
    {
        PlayerInRange = false;
        float distance = 0 ;
        for (int i = 0; i < Zones.Count; i++)
        {
            if (Zones[i].IsActive)
            {
                distance = (Vector3.Distance(PlayerObject.transform.position,Zones[i].LinkedObject.transform.position));
                if (NearestRelayDistance <  distance) 
                {
                    NearestRelayDistance = distance;
                    NearestRelay = Zones[i].LinkedObject;
                }
                PlayerInRange = PlayerInRange |(distance <= Zones[i].Radius);
            }
        }
    }

    public override void Update()
    {
        if (PlayerObject == null) return;//don't run checks if there is no linked player!
        CommunicationUpdate();

        if (!PlayerInRange)
        {
            Debug.Log("Left Comm range");
        }
        if (!PlayerInRange && OnLoseConnection != null)
        {
            OnLoseConnection(NearestRelay);
        }
    }

    public override void Reset()
    {
        Zones.Clear();
    }
}
