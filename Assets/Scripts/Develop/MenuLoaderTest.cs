using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoaderTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameFrameworkManager Gamemanager = null;
    [SerializeField] private string SceneName = null;
    void Start()
    {
        Gamemanager.LoadSceneByName(SceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
