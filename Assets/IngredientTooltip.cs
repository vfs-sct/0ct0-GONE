using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class IngredientTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title = null;
    [SerializeField] TextMeshProUGUI desc = null;
    [SerializeField] TextMeshProUGUI requires = null;
    [SerializeField] HorizontalLayoutGroup contentGroup = null;
    [SerializeField] GameObject subIngredientBox = null;

    private Dictionary<TextMeshProUGUI, Resource> TextToResource = new Dictionary<TextMeshProUGUI, Resource>();
    private Dictionary<TextMeshProUGUI, Item> TextToItem = new Dictionary<TextMeshProUGUI, Item>();

    private ResourceInventory shipInventory = null;
    private InventoryController playerInventory = null;

    private void OnEnable()
    {
        UpdateOwnedAmounts();
    }

    //hooked up in editor to mouse trigger
    public void ShowItemTooltip()
    {
        gameObject.SetActive(true);
    }
    //hooked up in editor to mouse trigger
    public void HideItemTooltip()
    {
        gameObject.SetActive(false);
    }

    public void SetTitle(string Title)
    {
        title.SetText(Title);
    }

    public TextMeshProUGUI GetTitle()
    {
        return title;
    }
    public void SetDesc(string Desc)
    {
        desc.SetText(Desc);
    }
    public GameObject GetHorizontalLayout()
    {
        return subIngredientBox;
    }

    public void AddSubIngredients(Recipe recipe, ResourceInventory shipInv, InventoryController playerInv)
    {
        shipInventory = shipInv;
        playerInventory = playerInv;

        requires.gameObject.SetActive(true);

        foreach (var resourceInp in recipe.ResourceInput)
        {
            var subIngred = Instantiate(subIngredientBox);
            subIngred.transform.SetParent(contentGroup.transform);

            subIngred.GetComponentInChildren<Image>().sprite = resourceInp.resource.resourceIcon;
            var text = subIngred.GetComponentsInChildren<TextMeshProUGUI>();
            text[0].SetText(resourceInp.resource.DisplayName);
            text[0].color = resourceInp.resource.ResourceColor;
            TextToResource.Add(text[1], resourceInp.resource);
            text[2].SetText($"/ {resourceInp.amount.ToString()}");
        }

        foreach (var ingredient in recipe.ItemInput)
        {
            var subIngred = Instantiate(subIngredientBox);
            subIngred.transform.SetParent(contentGroup.transform);

            subIngred.GetComponentInChildren<Image>().sprite = ingredient.item.Icon;
            var text = subIngred.GetComponentsInChildren<TextMeshProUGUI>();
            text[0].SetText(ingredient.item.Name);
            TextToItem.Add(text[1], ingredient.item);
            text[2].SetText($"/ {ingredient.amount.ToString()}");
        }
        UpdateOwnedAmounts();
    }

    private void UpdateOwnedAmounts()
    {
        foreach(var kvp in TextToResource)
        {
            kvp.Key.SetText(shipInventory.GetResource(kvp.Value).ToString());
        }
        foreach (var kvp in TextToItem)
        {
            int amount = playerInventory.GetItemAmount(kvp.Value);
            if (amount < 0) amount = 0;
            kvp.Key.SetText(amount.ToString());
        }
    }
}
