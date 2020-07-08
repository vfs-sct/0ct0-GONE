using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWarnings : MonoBehaviour
{
    [SerializeField] private GameObject[] warnings;
    // Start is called before the first frame update
    
    public GameObject GetWarning(int index)
    {
        return warnings[index];
    }
}
