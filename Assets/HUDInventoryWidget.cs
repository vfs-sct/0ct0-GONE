using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HUDInventoryWidget : MonoBehaviour
{
    [SerializeField] UIAwake UIRoot = null;
    [SerializeField] HorizontalLayoutGroup buttonPanel = null;
    [SerializeField] GameObject defaultDial = null;

    [SerializeField] public Resource[] resources = null;
    
    private Dictionary<Resource, Image> updateFill = new Dictionary<Resource, Image>();

    private InventoryController bucketInventory;

    //used mainly by tutorial prompts to teach folks to salvage
    public int CalculateTotalMass()
    {
        int newTotalMass = 0;
        foreach (var entry in resources)
        {
            newTotalMass += bucketInventory.GetResourceAmount(entry);
        }
        return newTotalMass;
    }

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
        newDial.GetComponent<GetObjectsDial>().GetText().SetText(resource.Abreviation);
        newDial.GetComponent<GetObjectsDial>().GetText().color = resource.ResourceColor;
        newDial.GetComponent<GetObjectsDial>().GetBKImage().color = new Color(resource.ResourceColor.r, resource.ResourceColor.g, resource.ResourceColor.b, 0.2f);
        
        var fillImage = newDial.GetComponent<GetObjectsDial>().GetFillImage();
        fillImage.color = resource.ResourceColor;
        fillImage.fillAmount = bucketInventory.GetResourceAmount(resource);
        updateFill.Add(resource, fillImage);
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var kvp in updateFill)
        {
            //Debug.Log((float)bucketInventory.GetResourceAmount(kvp.Key) / 10);
            kvp.Value.fillAmount = ((float)bucketInventory.GetResourceAmount(kvp.Key) / 100);
        }
    }
}
