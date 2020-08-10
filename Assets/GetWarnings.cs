using UnityEngine;

public class GetWarnings : MonoBehaviour
{
    [SerializeField] private GameObject[] warnings = null;
    // Start is called before the first frame update
    
    public GameObject GetWarning(int index)
    {
        return warnings[index];
    }
}
