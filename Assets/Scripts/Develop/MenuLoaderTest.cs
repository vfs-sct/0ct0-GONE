using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableGameFramework;
public class MenuLoaderTest : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string SceneName = null;
    void Start()
    {
        Game.Manager.LoadScene(SceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
