using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField] public UIAwake UIRoot = null;
    [SerializeField] private Resource[] resources = null;
    [SerializeField] public VerticalLayoutGroup contentGroup = null;
    [SerializeField] public GameObject defaultText = null;
    [SerializeField] private ResourceModule ResourceController = null;
    Dictionary<Resource, GameObject> resourceList = new Dictionary<Resource, GameObject>();

    private ResourceInventory playerInventory;

    void Awake()
    {
        playerInventory = UIRoot.GetPlayer().GetComponent<ResourceInventory>();

        foreach (var resource in resources)
        {
            var currentAmount = playerInventory.GetResource(resource);
            var newLine = Instantiate(defaultText);

            newLine.transform.SetParent(contentGroup.transform);

            newLine.GetComponent<TextMeshProUGUI>().SetText(resource.DisplayName + ": " + currentAmount);

            //save an association between the resource and the new entry we've created so we can pick out
            //specific resources to update later
            resourceList.Add(resource, newLine);
            ResourceController.RegisterOnAddDelegate(resource,playerInventory,UpdateResourceAmount);
        }
    }

    public void UpdateResourceAmount(Resource resource, float DeltaValue)
    {
        var updateLine = resourceList[resource];

        updateLine.GetComponent<TextMeshProUGUI>().SetText(resource.DisplayName + ": " + playerInventory.GetResource(resource));
    }
}
