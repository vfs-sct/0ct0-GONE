using UnityEngine;

public class EnterAreaWarning : MonoBehaviour
{
    [SerializeField] UIModule UIModule = null;
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] GameState playing = null;
    [SerializeField] string warningType = null;
    private GameObject alert = null;
    private Player player;

    private void Start()
    {
        if (warningType == "GasCloud")
        {
            alert = UIModule.UIRoot.GetScreen<GameHUD>().GasCloudAlertPrefab;
        }
        else if (warningType == "CommRange")
        {

        }
        player = UIModule.UIRoot.player;
    }

    private void Update()
    {
        if(GameManager.ActiveGameState == playing && player == null)
        {
            player = UIModule.UIRoot.player;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            alert.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            alert.SetActive(false);
        }
    }
}
