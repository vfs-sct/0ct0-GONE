using UnityEngine;

[CreateAssetMenu(menuName = "Systems/SaveFile")]

public class SaveFile : ScriptableObject
{
    //number of audio logs already unlocked
    public int objective = 0;

    //how much of each resource is in the space station
    public float[] hubResource = new float[5] { 0f, 0f, 0f, 0f, 0f };

    public void Save()
    {
        using (var file = new System.IO.StreamWriter(@"0CT0-save.sav"))
        {
            file.WriteLine($"objective={objective}");

            for(int i = 0; i < hubResource.Length; i++)
            {
                file.WriteLine($"hubResource{i}={hubResource[i]}");
            }
        }
    }

    public void Load()
    {
        if (System.IO.File.Exists(@"0CT0-save.sav"))
        {
            foreach (var line in System.IO.File.ReadAllLines(@"0CT0-save.sav"))
            {
                if (line.StartsWith("objective="))
                {
                    objective = int.Parse(line.Split('=')[1]);
                    return;
                }

                for(int i = 0; i < hubResource.Length; i++)
                {
                    if (line.StartsWith($"hubResource{i}="))
                    {
                        hubResource[i] = int.Parse(line.Split('=')[1]);
                        return;
                    }
                }
            }
        }
        else
        {
            //not necessarily an error, the player may just not have done anything that saved yet
            Debug.Log("No save to load");
        }
    }

    //is there currently a game in progress
    public bool HasSaveGame()
    {
        if (System.IO.File.Exists(@"0CT0-save.sav"))
        {
            return true;
        }

        return false;
    }

    public void Reset()
    {
        System.IO.File.Delete(@"0CT0-save.sav");

        objective = 0;

        for(int i = 0; i < hubResource.Length; i++)
        {
            hubResource[i] = 0f;
        }
    }
}
