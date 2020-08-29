using UnityEngine;
using UnityEngine.UI;

public class LowFuel : MonoBehaviour
{
    [SerializeField] private FlashWhileActive flashScript = null;
    [SerializeField] private GameFrameworkManager gameManager = null;

    private Image thisImage = null;
    private Color fadeTargetCol;

    private bool fadeOut = false;
    private float fadeOutTime = 1f;

    private void Start()
    {
        thisImage = this.gameObject.GetComponent<Image>();
        fadeTargetCol = new Color(thisImage.color.r, thisImage.color.g, thisImage.color.b, 0f);
    }

    public void TurnOn()
    {
        //EVAN - sound for when octo has low energy and is about to die - this is a continuous state
        //so it could be a single sound or a loop
        AkSoundEngine.PostEvent("FuelLow", gameObject);
        this.gameObject.SetActive(true);
        flashScript.enabled = true;
        fadeOut = false;
    }

    public void TurnOff()
    {
        //EVAN - if the "low energy" sound is a loop, this is where to end it
        fadeOut = true;
        flashScript.enabled = false;
    }

    public void FadeOut()
    {
        if(thisImage.color.a > 0.1f)
        {
            //flash stops animating if the game is paused, but animates out if the game's won/lost
            if (gameManager.ActiveGameState.GetType() == typeof(Playing))
            {
                thisImage.color = Color.Lerp(thisImage.color, fadeTargetCol, Time.deltaTime * 1f / fadeOutTime);
            }
            else
            {
                thisImage.color = Color.Lerp(thisImage.color, fadeTargetCol, Time.unscaledTime * 1f / fadeOutTime);
            }
        }
        else
        {
            fadeOut = false;
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(fadeOut)
       {
            FadeOut();
            //Debug.LogError(thisImage.color.a);
            return;
       }

        if (gameManager.ActiveGameState.GetType() != typeof(Playing))
        {
            fadeOut = true;
        }
    }
}
