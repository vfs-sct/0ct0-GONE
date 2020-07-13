using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/ComRelay/CommunicationsModule")]
public class CommunicationModule : Module
{

    public delegate void CommRelayEvent(CommunicationZone ActiveZone);

    private struct CommRelayData
    {
        public CommunicationZone LinkedObject;
        public float Radius;
        public bool IsActive;

        public bool ShowWarning;
        public GameObject RangeIndicator;

        public CommRelayData(CommunicationZone G,float R, bool A)
        {
            LinkedObject = G;
            Radius = R;
            IsActive = A;
            RangeIndicator = null;
            ShowWarning = false;
        }
        public CommRelayData(CommunicationZone G,float R, bool A,GameObject RA)
        {
            LinkedObject = G;
            Radius = R;
            IsActive = A;
            RangeIndicator = RA;
            ShowWarning = false;
        }
        public CommRelayData(CommRelayData Data,bool SW)
        {
            LinkedObject = Data.LinkedObject;
            Radius = Data.Radius;
            IsActive = Data.IsActive;
            RangeIndicator = Data.RangeIndicator;
            ShowWarning = SW;
        }

    }

    private CommRelayEvent OnLoseConnection = null;
    private GameObject PlayerObject = null;
    private bool PlayerInRange = true;
    public bool InRange{get=>PlayerInRange;}
    private CommunicationZone NearestRelay = null;
    private float NearestRelayDistance = 999999;

    private List<CommRelayData> Zones = new List<CommRelayData>();
    private GetWarnings warningUI = null;

    [SerializeField] private GameObject CommRelayRangeIndicatorPrefab;
    [SerializeField] private float WarningDistance = 20; 
    [SerializeField] private UIModule UIController;

    public override void Initialize()
    {

    }

    public override void Start()
    {
        warningUI = UIController.UIRoot.GetScreen<GetWarnings>();
        Debug.Assert(PlayerObject != null); //at this point the gamestate should have assigned these values
        RunUpdate = true;
    }

    //show and hide are used by CommunicationZone.cs when visibly expanding the radius when upgraded by the player
    public void ShowRangeIndicator(int index)
    {
        Zones[index].RangeIndicator.SetActive(true);
    }

    public void HideRangeIndicator(int index)
    {
        Zones[index].RangeIndicator.SetActive(false);
    }

    public int AddZone(CommunicationZone NewZone)
    {
        GameObject NewIndicator = GameObject.Instantiate(CommRelayRangeIndicatorPrefab);
        NewIndicator.transform.SetPositionAndRotation(NewZone.transform.position,NewZone.transform.rotation);
        NewIndicator.transform.localScale = new Vector3(NewZone.Radius,NewZone.Radius,NewZone.Radius);
        NewIndicator.SetActive(false);
        Zones.Add(new CommRelayData(NewZone,NewZone.Radius,NewZone.enabled,NewIndicator));
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
                if (Zones[i].ShowWarning == false && distance >= (Zones[i].Radius-WarningDistance))
                {
                    warningUI.GetWarning(0).SetActive(true);
                    ShowRangeIndicator(i);
                    Zones[i] = new CommRelayData(Zones[i],true);
                }
                else if (Zones[i].ShowWarning == true &&  distance < (Zones[i].Radius-WarningDistance))
                {
                    warningUI.GetWarning(0).SetActive(false);
                    HideRangeIndicator(i);
                    Zones[i] = new CommRelayData(Zones[i],false);
                }
            }
        }
    }

    public override void Update()
    {
        if (PlayerObject == null) return;//don't run checks if there is no linked player!
        CommunicationUpdate();

        //if (!PlayerInRange)
        //{
        //    Debug.Log("Left Comm range");
        //}
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
