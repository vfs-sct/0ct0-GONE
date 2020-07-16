using UnityEngine;

public class LowFuel : MonoBehaviour
{

    private bool fadeOut = false;
    [SerializeField] private FlashWhileActive flashScript = null;

    private void Start()
    {
        flashScript.enabled = false;
    }

    public void TurnOn()
    {
        this.gameObject.SetActive(true);
        flashScript.enabled = true;
    }

    public void TurnOff()
    {
        fadeOut = true;
        flashScript.enabled = false;
    }

    public void FadeOut()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if(fadeOut)
       {
            FadeOut();
       }
    }
}
