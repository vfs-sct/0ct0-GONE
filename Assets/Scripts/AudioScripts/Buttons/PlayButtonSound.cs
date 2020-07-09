using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    // Handle the button clicks
    public void OnClick()
    {
        AkSoundEngine.PostEvent("MainMenu_Button_Play",gameObject);
    }

    // Handles Mouse hovers
    public void OnMouseOver()
    {
        AkSoundEngine.PostEvent("MainMenu_All_Button_Hover", gameObject);
    }

    public void OnClickPlayDialogue(string fileName)
    {
        //TEMP_VO_Oakley1
        AkSoundEngine.PostEvent(fileName, gameObject);
    }
}
