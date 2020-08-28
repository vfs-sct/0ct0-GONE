using UnityEngine;

[CreateAssetMenu(menuName = "Systems/SaveFile")]

public class SaveFile : ScriptableObject
{
    //number of audio logs already unlocked
    public int objective = 0;

    //how much of each resource is in the space station
    public float[] hubResource = new float[5] { 0f, 0f, 0f, 0f, 0f };

    //is there currently a game in progress
    public bool HasSaveGame()
    {
        if(objective != 0)
        {
            return true;
        }

        foreach(var entry in hubResource)
        {
            if(entry != 0f)
            {
                return true;
            }
        }

        return false;
    }

    public void Reset()
    {
        objective = 0;

        for(int i = 0; i < hubResource.Length; i++)
        {
            hubResource[i] = 0f;
        }
    }
}
