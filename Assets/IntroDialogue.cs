using UnityEngine;

public class IntroDialogue : MonoBehaviour
{
    [SerializeField] private Codex codex;
    [SerializeField] private float startBufferTime = 2.2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait(startBufferTime));
    }

    System.Collections.IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        codex.UnlockNextEntry();
    }
}
