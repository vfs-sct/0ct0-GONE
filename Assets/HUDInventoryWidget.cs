using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HUDInventoryWidget : MonoBehaviour
{
    [SerializeField] UIAwake UIRoot = null;
    [SerializeField] HorizontalLayoutGroup buttonPanel = null;
    [SerializeField] GameObject defaultDial = null;

    [SerializeField] private Resource[] resources = null;
    
    private Dictionary<Resource, Image> updateFill = new Dictionary<Resource, Image>();

    private InventoryController bucketInventory;
    // Start is called before the first frame update
    void Start()
    {
        bucketInventory = UIRoot.GetPlayer().GetComponent<InventoryController>();

        foreach(var resource in resources)
        {
            GenerateDial(resource);
        }
    }

    private void GenerateDial(Resource resource)
    {
        var newDial = Instantiate(defaultDial);
        newDial.transform.SetParent(buttonPanel.transform);
        var fillImage = newDial.GetComponent<GetObjectsDial>().GetFillImage();
        fillImage.color = resource.ResourceColor;
        newDial.GetComponent<GetObjectsDial>().GetText().SetText(resource.DisplayName);
        updateFill.Add(resource, fillImage);
        fillImage.fillAmount = bucketInventory.GetFillAmount(resource);
        newDial.GetComponent<GetObjectsDial>().GetBKImage().color = resource.ResourceColor;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var kvp in updateFill)
        {
            kvp.Value.fillAmount = bucketInventory.GetFillAmount(kvp.Key);
        }
    }
}
