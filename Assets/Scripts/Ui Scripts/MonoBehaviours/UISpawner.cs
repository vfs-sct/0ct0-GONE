using UnityEngine;

public class UISpawner : MonoBehaviour
{
    [SerializeField] UIRoot UIRoot = null;
    [SerializeField] string startScreen = null;
    [SerializeField] UIModule UIModule = null;

    // Start is called before the first frame update
    void Awake()
    {
        UIModule.UIRoot = GameObject.Instantiate(UIRoot);
        UIModule.UIRoot.GetScreen(startScreen).SetActive(true);
    }
}