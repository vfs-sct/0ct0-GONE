using UnityEngine;

public class Damaged : MonoBehaviour
{
    [SerializeField] private float flashTime;

    private void OnEnable()
    {
        StartCoroutine(BufferTime(flashTime));
    }

    System.Collections.IEnumerator BufferTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }
}
