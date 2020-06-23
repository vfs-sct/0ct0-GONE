using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDInventoryWidget : MonoBehaviour
{
    [SerializeField] UIAwake UIRoot = null;
    [SerializeField] HorizontalLayoutGroup buttonPanel = null;
    [SerializeField] GameObject defaultDial = null;

    private ResourceInventory playerInventory;
    // Start is called before the first frame update
    void Awake()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
