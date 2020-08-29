using UnityEngine;

[CreateAssetMenu(menuName = "Systems/SaveFile")]

public class SaveFile : ScriptableObject
{
    //number of audio logs already unlocked
    [System.NonSerialized]
    public int objective = 0;

    //how much of each resource is in the space station
    [System.NonSerialized]
    public float[] hubResource = new float[5] { 0f, 0f, 0f, 0f, 0f };

    [System.NonSerialized]
    public bool[] repairables = new bool[4] { false, false, false, false };

    public void Save()
    {
        using (var file = new System.IO.StreamWriter(@"0CT0-save.sav"))
        {
            file.WriteLine($"objective={objective}");

            for(int i = 0; i < hubResource.Length; i++)
            {
                file.WriteLine($"hubResource{i}={hubResource[i]}");
            }

            for (int i = 0; i < repairables.Length; i++)
            {
                file.WriteLine($"repairables{i}={repairables[i]}");
            }
        }
    }

    public void Load()
    {
        if (System.IO.File.Exists(@"0CT0-save.sav"))
        {
            foreach (var line in System.IO.File.ReadAllLines(@"0CT0-save.sav"))
            {
                //========= CURRENT OBJECTIVE =========

                if (line.StartsWith("objective="))
                {
                    objective = int.Parse(line.Split('=')[1]);
                }

                //========= RESOURCES IN HUB =========

                for (int i = 0; i < hubResource.Length; i++)
                {
                    if (line.StartsWith($"hubResource{i}="))
                    {
                        hubResource[i] = float.Parse(line.Split('=')[1]);
                    }
                }

                //========= STATION REPAIRABLES =========

                for (int i = 0; i < repairables.Length; i++)
                {
                    if (line.StartsWith($"repairables{i}="))
                    {
                        repairables[i] = bool.Parse(line.Split('=')[1]);
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

        for (int i = 0; i < repairables.Length; i++)
        {
            repairables[i] = false;
        }
    }
}
