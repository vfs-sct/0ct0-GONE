using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField] public ResourceInventory playerInventory = null;
    [SerializeField] private Resource[] resources = null;
    [SerializeField] public VerticalLayoutGroup contentGroup = null;
    [SerializeField] public GameObject defaultText = null;

    Dictionary<Resource, GameObject> resourceList;
    
    // Start is called before the first frame update
    void Awake()
    {
        foreach(var resource in resources)
        {
            var currentAmount = playerInventory.GetResource(resource);
            var newLine = Instantiate(defaultText);

            newLine.transform.SetParent(contentGroup.transform);

            newLine.GetComponent<TextMeshProUGUI>().SetText(resource.DisplayName + ": " + currentAmount);

            //save an association between the resource and the new entry we've created so we can pick out
            //specific resources to update later
            resourceList.Add(resource, newLine);
        }
    }

    void UpdateResourceAmount(Resource resource, float newAmount)
    {
        var updateLine = resourceList[resource];

        updateLine.GetComponent<TextMeshProUGUI>().SetText(resource.DisplayName + ": " + newAmount);
    }
}
