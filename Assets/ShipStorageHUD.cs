using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipStorageHUD : MonoBehaviour
{
    //storage owner is the ship whose inventory we want to display
    [SerializeField] ResourceInventory storageOwner = null;
    [SerializeField] HorizontalLayoutGroup dialLayout = null;
    [SerializeField] GameObject dial = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
