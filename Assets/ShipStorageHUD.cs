using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShipStorageHUD : MonoBehaviour
{
    //storage owner is the ship whose inventory we want to display
    [SerializeField] ResourceInventory storageOwner = null;
    [SerializeField] HorizontalLayoutGroup dialLayout = null;
    [SerializeField] GameObject dial = null;

    private List<Resource> resourceList = null;
    private Dictionary<Resource, GameObject> updateDial = new Dictionary<Resource, GameObject>();

    //Start is called before the first frame update
    void Start()
    {
        resourceList = storageOwner.GetActiveResourceList();
        foreach (var resource in resourceList)
        {
            GenerateDial(resource);
        }
    }

    public void SetStorageOwner(ResourceInventory newOwner)
    {
        storageOwner = newOwner;
    }

    private void OnEnable()
    {
        if(resourceList == null)
        {
            resourceList = storageOwner.GetActiveResourceList();
        }

        UpdateDials();
    }

    private void GenerateDial(Resource resource)
    {
        var newDial = Instantiate(dial);

        newDial.transform.SetParent(dialLayout.transform);

        var dialText = newDial.GetComponent<GetObjectsDial>().GetText();
        dialText.SetText($"{resource.DisplayName} ({resource.Abreviation})");
        dialText.color = resource.ResourceColor;

        var capacityText = newDial.GetComponent<GetObjectsDial>().GetCapacityText();
        capacityText.SetText($"{storageOwner.GetResource(resource).ToString()}/{resource.GetMaximum().ToString()}");
        capacityText.color = resource.ResourceColor;

        newDial.GetComponent<GetObjectsDial>().GetBKImage().color = new Color(resource.ResourceColor.r, resource.ResourceColor.g, resource.ResourceColor.b, 0.2f);

        var fillImage = newDial.GetComponent<GetObjectsDial>().GetFillImage();
        fillImage.color = resource.ResourceColor;
        fillImage.fillAmount = storageOwner.GetResource(resource) / resource.GetMaximum();
        updateDial.Add(resource, newDial);

    }

    public void UpdateDials()
    {
        foreach (var kvp in updateDial)
        {
            var getObjects = kvp.Value.GetComponent<GetObjectsDial>();

            getObjects.GetCapacityText().SetText($"{storageOwner.GetResource(kvp.Key).ToString()}/{kvp.Key.GetMaximum().ToString()}");
            getObjects.GetFillImage().fillAmount = storageOwner.GetResource(kvp.Key) / kvp.Key.GetMaximum();
        }
    }
}
