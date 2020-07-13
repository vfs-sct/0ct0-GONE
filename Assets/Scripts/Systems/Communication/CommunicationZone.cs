using UnityEngine;


public class CommunicationZone : MonoBehaviour
{
    [SerializeField] private EventModule EventModule = null;
    [SerializeField] private CommunicationModule CommunicationManager = null;

    [SerializeField] private float _Radius = 5000;

    [Header("Expansion Effect")]
    [SerializeField] private float expandTime = 2f;
    private bool isExpanding = false;
    private float targetRadius;
    public float Radius{get=>_Radius;}

    private int ZoneIndex = -1;

    void Start()
    {
        ZoneIndex = CommunicationManager.AddZone(this);
        CommunicationManager.ShowRangeIndicator(0);
        EventModule.SetCommZone(this);
    }

    private void Update()
    {
        if(isExpanding)
        {
            ExpandRange();
        }
    }

    public void AddRange(float addRange)
    {
        targetRadius = _Radius + addRange;
        isExpanding = true;
        CommunicationManager.ShowRangeIndicator(0);
        Debug.Log("New comm range: {_Radius}");
    }

    //when an event extends the comm range, make the indicator visible and show them the range expanding out
    public void ExpandRange()
    {
        if(_Radius >= targetRadius - 5f)
        {
            _Radius = targetRadius;
            CommunicationManager.HideRangeIndicator(0);
            isExpanding = false;
            return;
        }
        _Radius = Mathf.SmoothStep(_Radius, targetRadius, Time.deltaTime * 1f / expandTime);
    }
}
