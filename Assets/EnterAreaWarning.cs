using UnityEngine;
using ScriptableGameFramework;
public class EnterAreaWarning : MonoBehaviour
{
    [SerializeField] UIModule UIModule = null;
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] GameState playing = null;
    [SerializeField] string warningType = null;

    [Header("Health Resource")]
    [SerializeField] public Resource health = null;

    [Header("Damage Per Second")]
    [SerializeField] public float dmgPerSecond;
    private ActivateWarning alert = null;
    private Player player;

    private ResourceInventory playerInventory;

    private bool inGasCloud = false;
    
    private void Start()
    {
        if (warningType == "GasCloud")
        {
            alert = UIModule.UIRoot.GetScreen<GameHUD>().GasCloudAlertPrefab.GetComponent<ActivateWarning>();
        }
        player = UIModule.UIRoot.player;
        playerInventory = UIModule.UIRoot.player.GetComponent<ResourceInventory>();
    }

    private void Update()
    {
        if (inGasCloud)
        {
            playerInventory.RemoveResource(health, dmgPerSecond * Time.deltaTime);
        }
    }

    //turn on warning when you enter
    private void OnTriggerEnter(Collider other)
    {
        if(player == null)
        {
            Debug.Log("EnterAreaWarning did not find a player");
        }
        else if (other.gameObject == player.gameObject)
        {
            alert.gameObject.SetActive(true);
            AkSoundEngine.PostEvent("GasDetected", gameObject);
            inGasCloud = true;
        }
    }

    //turn off warning when you leave
    //NOTE: currently turns warning off abruptly, no fading
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            alert.DisableWarning();
            inGasCloud = false;
        }
    }
}
