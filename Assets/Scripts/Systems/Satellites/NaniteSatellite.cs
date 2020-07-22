using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaniteSatellite : MonoBehaviour
{

    [SerializeField] private ResourceInventory LinkedInventory;

    [SerializeField] private Resource NaniteResource;

    [SerializeField] private float NanitesPerSecond = 1;
    [SerializeField] private float UpdatesPerSecond = 10;

    [SerializeField] private float NaniteOffloadAmount = 5;

    [SerializeField] private string OffloadEmptyMsg = "No Nanites Left";
    [SerializeField] private string OffloadFullMsg = "Nanites Full";

    private float NextTimeShiftValue;
    private float NanitesPerTick;

    private float NextUpdate = 0;

    // Start is called before the first frame update
    void Start()
    {
        NextTimeShiftValue = (1.0f/UpdatesPerSecond);
        NanitesPerTick = NanitesPerSecond/UpdatesPerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= NextUpdate)
        {
            LinkedInventory.AddResource(NaniteResource,NanitesPerTick);
            NextUpdate = Time.time+ NextTimeShiftValue;
        }
    }

    public void TryOffload(ResourceInventory Target)
    {
        string temp;
        TryOffload(Target,out temp);
    }


    public void TryOffload(ResourceInventory Target, out string error)
    {
        if (Target.CanAdd(NaniteResource,NaniteOffloadAmount))
        {
            if (LinkedInventory.GetResource(NaniteResource)>= NaniteOffloadAmount)
            {
                Target.AddResource(NaniteResource,NaniteOffloadAmount);
                LinkedInventory.RemoveResource(NaniteResource,NaniteOffloadAmount);
                Debug.Log("Offloading Nanites:");
                Debug.Log(Target.GetResource(NaniteResource));
            }
            else{
                Debug.Log("Not enough Nanites");
                error = OffloadEmptyMsg;
                return;
            }
            
        }
        else{
                error = OffloadFullMsg;
                Debug.Log("Player Inv Full");
                return;
        }
        error = null;
    }

}
