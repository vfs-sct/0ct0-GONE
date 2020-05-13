using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Resources/New Resource")]
public class Resource : ScriptableObject
{
    public delegate void ResourceEvent(ResourceBehavior Caller);

    public delegate void ResourceEventDelta(ResourceBehavior Caller,float ResourceDelta);


    [System.Serializable]
    public struct ResourceData
    {
        public Resource resource;
        public float Amount;

        public float Min;

        public float Max;
        public ResourceData(Resource R, float A,float M,float MX)
        {
            resource = R;
            Amount = A;
            Min = M;
            Max = MX;
        }
        public void AddAmount(float _amount)
        {
            Amount += _amount;
        }
        public void SubAmount(float _amount)
        {
            AddAmount(-_amount);
        }
        public void SetAmount(float newAmount)
        {
            Amount = newAmount;
        }
    }

    public string Name{get=>this.ToString();}

}
