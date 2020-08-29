using UnityEngine;
using UnityEngine.InputSystem;
using ScriptableGameFramework;
public class NaniteRefill : MonoBehaviour
{
    [SerializeField] GameFrameworkManager GameManager = null;
    [SerializeField] UIModule UIModule = null;
    [SerializeField] Player Player = null;
    [SerializeField] private ResourceInventory PlayerInv;

    private bool isRefilling = false;

    private NaniteSatellite TargetSat = null;
    
    
    
    public void OnRefuelHotkey(InputAction.CallbackContext context)
    {
        if (Player.mouseCollisionRoot == null) return;
        
        
        TargetSat = Player.mouseCollisionRoot.GetComponent<NaniteSatellite>();

        Debug.Log(TargetSat);
        if (context.performed)
        {
             Debug.Log("Performed");
            
            isRefilling = context.performed && TargetSat != null;
        }
        if (context.canceled)
        {
            Debug.Log("Released");
            TargetSat = null;
            isRefilling = false;
        }
        
         
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRefilling)
        {
            TargetSat.TryOffload(PlayerInv);
        }
    }
}
