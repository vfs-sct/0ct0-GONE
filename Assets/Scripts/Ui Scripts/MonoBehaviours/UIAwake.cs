using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.UI;

public class UIAwake : MonoBehaviour
{
    [SerializeField] GameObject DebugPrefab = null;
    [SerializeField] public float gammaDefault = 2.2f;
    
    private Player player = null;
    // Start is called before the first frame update
   
    public Player GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindObjectOfType<Player>();
        }
        return player;
    }

    void Start()
    {
        var camera = Camera.main;

        foreach (var canvas in GameObject.FindObjectsOfType<Canvas>())
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = camera;
        }

        camera.gameObject.AddComponent<PostProcessing>().material = Resources.Load<Material>("GammaMaterial");

        if (Shader.GetGlobalFloat("gamma") == 0)
        {
            Shader.SetGlobalFloat("gamma", gammaDefault);
        }

    }

    public void OnDebug(InputValue value)
    {
        if (!DebugPrefab.activeSelf)
        {
            DebugPrefab.SetActive(true);
        }
    }
}
