using UnityEngine;

public class GetTooltip : MonoBehaviour
{

    [SerializeField] IngredientTooltip tooltip = null;
    

    public IngredientTooltip GetTooltipScript()
    {
        return tooltip;
    }
}
