using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    // Handle the button clicks
    public void OnClick()
    {
        AkSoundEngine.PostEvent("MainMenu_Button_Play",gameObject);
    }

    // Handles Mouse hovers
    private void OnMouseOver()
    {
        AkSoundEngine.PostEvent("MainMenu_All_Button_Hover", gameObject);
    }

    public void OnClickPlayDialogue()
    {
        AkSoundEngine.PostEvent("TEMP_VO_Oakley1", gameObject);
    }
}
