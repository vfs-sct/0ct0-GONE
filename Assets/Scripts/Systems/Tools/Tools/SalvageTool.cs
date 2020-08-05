using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Systems/Tools/Salvager")]
public class SalvageTool : Tool
{
    [SerializeField] public ResourceGainedPopTxt popText = null;
    [SerializeField] private UIModule UIModule = null;


    private bool finishedSalvage = false;

    private float salvageTime = 1.0f;

    Salvagable SalvComp;

    GameObject OriginalTarget;

    bool OutOfRange = false;
    bool SwitchedTargets = false;
    bool FinishedSalvage = false;

    float SalvageStartTime = 0;

    private Image progressBarBG = null;
    private Image progressBarFill = null;
    protected override bool ActivateCondition(ToolController owner, GameObject target)
    {
        //Debug.Log(target);
        if (target != null && target.GetComponent<Salvagable>() != null)
        {
            //Debug.Log("Salvage Found");
            return true;
        }
        return false;
    }

    protected override bool DeactivateCondition(ToolController owner, GameObject target)
    {
        return true;
    }

    protected override bool LoopCondition(ToolController owner, GameObject target)
    {
        SwitchedTargets = (OriginalTarget != target);
        if (target == null) return false;

        OutOfRange = Vector3.Distance(target.transform.position,owner.transform.position) >= _ToolRange;
        if(OutOfRange)
        {
            progressBarFill.fillAmount = 0f;
            ToggleBars(false);
        }
        if(!OutOfRange && progressBarBG.gameObject.activeSelf == false)
        {
            progressBarFill.fillAmount = 0f;
            ToggleBars(true);
        }

        FinishedSalvage = ((SalvageStartTime+salvageTime) <= Time.unscaledTime);
        
        if(!FinishedSalvage && !OutOfRange)
        {
            progressBarFill.fillAmount = ((Time.unscaledTime - SalvageStartTime) / (salvageTime));
            //Debug.LogWarning(((Time.unscaledTime - SalvageStartTime )/ (salvageTime)));
        }
        else if(SwitchedTargets || FinishedSalvage || OutOfRange)
        {
            progressBarFill.fillAmount = 0f;
            ToggleBars(false);
        }

        return !(OutOfRange || SwitchedTargets || FinishedSalvage);
    }

    protected override void OnActivate(ToolController owner, GameObject target)
    {
        Debug.Log("Activated");
        SalvComp = target.GetComponent<Salvagable>();
        if (SalvComp.Instanced)
        {

            Rigidbody TargetRB = target.GetComponent<Rigidbody>();
            target.SetActive(false);
            GameObject NewSalvageObj = GameObject.Instantiate(SalvComp.UnInstancedPrefab);
            NewSalvageObj.transform.position = target.transform.position;
            NewSalvageObj.transform.rotation = target.transform.rotation;
            Rigidbody NewRB = NewSalvageObj.GetComponent<Rigidbody>();
            NewRB.velocity = TargetRB.velocity;
            NewRB.angularVelocity = TargetRB.angularVelocity;
            target = NewSalvageObj;
            owner.GetComponent<Player>().SetTarget(NewSalvageObj);
            TargetRB = NewRB;
        }







        OriginalTarget = target;
        salvageTime = SalvComp.SalvageItem.SalvageTime;
        SalvageStartTime = Time.unscaledTime;
        if (SalvComp.SalvageItem.IsSalvage)
        {
            if (!owner.PlayerInventory.CanAddToResourceBucket(SalvComp.SalvageItem,SalvComp.Amount))
            {
                Instantiate(popText).popText.SetText(SalvComp.SalvageItem.ResourceType.DisplayName + " Full");
                Deactivate(owner,target);
                return;
            }
        }
        else 
        {
            //error pop text
            Instantiate(popText).popText.SetText("Cannot Salvage");
            return;
            //Debug.Log("Could not salvage");
            //Debug.Log(SalvComp.SalvageItem + " is not a resource");
        }
        //EVAN: Start the salvage sound
        AkSoundEngine.PostEvent("SFX_Debris_Select", target);
    }

    protected override void OnDeactivate(ToolController owner, GameObject target)
    {
        if (SalvComp == null | SwitchedTargets |!FinishedSalvage | OutOfRange) return;
        Debug.Log("Deactivated");

        if (owner.PlayerInventory.AddToResourceBucket(SalvComp.SalvageItem,SalvComp.Amount))
            {
                //Debug.Log(owner.PlayerInventory.GetResourceAmount(SalvComp.SalvageItem.ResourceType));
                Destroy(OriginalTarget);

            //EVAN: Play salvaged success sound here
            AkSoundEngine.PostEvent("SFX_Debris_Collect", target);
            //resource gained pop text
            Instantiate(popText).popText.SetText(SalvComp.SalvageItem.ResourceType.DisplayName + " Gained");
                //Debug.Log("Salvaged Object");
            } 
        else 
        {
            Instantiate(popText).popText.SetText(SalvComp.SalvageItem.ResourceType.DisplayName + " Full");
        }

        //EVAN: finish the salvage sound  
        //error pop text
        //Debug.Log("Not enough space");
        SalvComp = null;
        SwitchedTargets = true;
        OriginalTarget = null;
        
    }

    protected override void OnSelect(ToolController owner)
    {
        //Debug.Log("Salvager Selected");
        progressBarBG = UIModule.UIRoot.GetScreen<GameHUD>().progressBarBG;
        progressBarFill = UIModule.UIRoot.GetScreen<GameHUD>().progressBarFill;
        //progressBarBG.gameObject.SetActive(true);
        //progressBarFill.gameObject.SetActive(true);

        progressBarFill.fillAmount = 0f;
    }

    protected override void OnDeselect(ToolController owner)
    {
        //Debug.Log("Salvager DeSelected");
        SalvComp = null; //cleanup
        ToggleBars(false);
        progressBarBG = null;
        progressBarFill = null;
    }

    private void ToggleBars(bool isOn)
    {
        progressBarBG.gameObject.SetActive(isOn);
        progressBarFill.gameObject.SetActive(isOn);
    }

    protected override void OnWhileActive(ToolController owner, GameObject target)
    {
        //Debug.LogWarning("Active");
        //EVAN: While loop when tool is active, play some laser noises
    }
}
