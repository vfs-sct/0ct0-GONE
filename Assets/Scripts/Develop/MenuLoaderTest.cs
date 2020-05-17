using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoaderTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameFrameworkManager Gamemanager;
    [SerializeField] private string SceneName;
    void Start()
    {
        Gamemanager.LoadSceneByName(SceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
