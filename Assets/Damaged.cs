using UnityEngine;
using ScriptableGameFramework;

public class Damaged : MonoBehaviour
{
    [SerializeField] private float flashTime;

    [SerializeField] private FillBar healthBar = null;

    private bool isLowFuel = false;

    private void Update()
    {
        if (Game.Manager.ActiveGameState.GetType() != typeof(Playing))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (!isLowFuel)
        {
            healthBar.Damaged();
            //EVAN - octo hurt sound, already buffered so cant be spammed
            AkSoundEngine.PostEvent("Damage", gameObject);
            StartCoroutine(BufferTime(flashTime));
        }
    }

    System.Collections.IEnumerator BufferTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }
}
