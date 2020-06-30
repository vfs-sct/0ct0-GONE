using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    public void OnClick()
    {
        AkSoundEngine.PostEvent ("MainMenu_Button_Play",gameObject);
    }

    public void OnClickPlayDialogue()
    {
        AkSoundEngine.PostEvent("TEMP_VO_Oakley1", gameObject);
    }
}
