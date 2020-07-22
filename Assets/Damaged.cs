using UnityEngine;

public class Damaged : MonoBehaviour
{
    [SerializeField] private float flashTime;
    [SerializeField] GameFrameworkManager gameManager = null;

    private bool isLowFuel = false;

    private void Update()
    {
        if (gameManager.ActiveGameState.GetType() != typeof(Playing))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (!isLowFuel)
        {
            //EVAN - octo hurt sound, already buffered so cant be spammed
            StartCoroutine(BufferTime(flashTime));
        }
    }

    System.Collections.IEnumerator BufferTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }
}
