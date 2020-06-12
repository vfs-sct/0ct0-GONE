using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAudioPlayer : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event PlayThrusterAudio;
    [SerializeField] private AK.Wwise.Event StopThrusterAudio;
    [SerializeField] private AK.Wwise.RTPC ThrusterImpulseRTPC;

    [SerializeField] private SpaceMovement Movement;

    private void Start()
    {
        PlayThrusterAudio.Post(gameObject);
    }

    private void Update()
    {
        ThrusterImpulseRTPC.SetGlobalValue(Mathf.Abs(Movement.Throttle.x + Movement.Throttle.y + Movement.Throttle.z) / 3);
        Debug.Log(ThrusterImpulseRTPC.GetGlobalValue());
        Debug.Log("==== Setting RTCP ====");
    }

    private void OnDestroy()
    {
        StopThrusterAudio.Post(gameObject);
    }
}
