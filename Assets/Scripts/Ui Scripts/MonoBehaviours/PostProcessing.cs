using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This will used to set the game's gamma using a custom shader
public class PostProcessing : MonoBehaviour
{
    //Put the custom shader in here
    public Material material;

    //"source" is the image of the screen we're taking
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
