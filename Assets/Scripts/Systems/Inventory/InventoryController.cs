using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [System.Serializable]
    public struct ItemBucket
    {
        public int ItemCap;

        public int FillAmount;
        public Dictionary<Item, int> Bucket;
        public int Count { get => Bucket.Count; }


        public ItemBucket(int Cap)
        {
            ItemCap = Cap;
            Bucket = new Dictionary<Item, int>();
            FillAmount = 0;
        }
        public ItemBucket AddBucketItem(Item newItem, out bool Success, int amount = 1)
        {
            if (FillAmount + (amount * newItem.Size) > ItemCap)
            {
                Success = false;
                return this;
            }
            if (Bucket == null)
            {
                Bucket = new Dictionary<Item, int>();
            }

            if (!Bucket.ContainsKey(newItem))
            {
                Bucket[newItem] = amount;
            }
            else
            {
                Bucket[newItem] += amount;
            }
            FillAmount += (amount * newItem.Size);
            Success = true;
            return this;
        }
        public ItemBucket GetBucketItemCount(Item getItem, out int amount)
        {
            if (Bucket == null || !Bucket.ContainsKey(getItem))
            {
                amount = -1;
            }
            else
            {
                amount = Bucket[getItem];
            }
            return this;
        }
        public ItemBucket RemoveBucketItem(Item remItem, out bool success, int amount = 1)
        {
            if (!Bucket.ContainsKey(remItem))
            {
                success = false;
                return this;
            }
            if (Bucket[remItem]-amount < 0)
            {
                success = false;
                return this;
            }
            Bucket[remItem] -= amount;
            FillAmount -= amount * remItem.Size;
            success = true;
            Debug.Log("Removed item");
            return this;
        }
    }

    [System.Serializable]
    public struct ResourceBucketData
    {
        public Resource ItemResource;
        public int Cap;

        public int StoredAmount;
        public ResourceBucketData(Resource RS, int C)
        {
            ItemResource = RS;
            Cap = C;
            StoredAmount = 0;
        }
    }


    [SerializeField] private float InventoryMassMultiplier = 1.0f;

    [SerializeField] private List<ItemBucket> ItemBuckets = new List<ItemBucket>();

    //this is for serialization
    [SerializeField] private List<ResourceBucketData> ResourceBuckets = new List<ResourceBucketData>();

    private Dictionary<Resource, ItemBucket> ResourceBuckets_Dict = new Dictionary<Resource, ItemBucket>();

    private Rigidbody playerRB;

    private float CarriedItemMassRaw = 0;

    private float CarriedResourceMassRaw = 0;

    [SerializeField] private Playing playingGameState = null;
    private Player player = null;

    private int totalInvWeight = 0;

    public void Start()
    {
        player = playingGameState.ActivePlayer;
    }

    public bool CheckIfItemBucket()
    {
        if (ItemBuckets[0].Bucket == null || ItemBuckets[0].Bucket.Count == 0)
        {
            //Debug.LogWarning("No item bucket");
            return false;
        }
        return true;
    }

    public bool CheckIfResourceBucket()
    {
        if (ResourceBuckets == null || ResourceBuckets.Count == 0)
        {
            Debug.LogWarning("No resource bucket");
            return false;
        }
        return true;
    }

    public List<ItemBucket> GetItemBucket()
    {
        return ItemBuckets;
    }

    public ItemBucket GetResourceBucket(Resource resource)
    {
        return ResourceBuckets_Dict[resource];
    }

    public int GetResourceAmount(Resource resource)
    {
        return ResourceBuckets_Dict[resource].FillAmount;
    }

    public int GetResourceCap(Resource resource)
    {
        return ResourceBuckets_Dict[resource].ItemCap;
    }

    public int GetItemBucketFillAmount(int ItemBucketIndex = 0)
    {
        return ItemBuckets[ItemBucketIndex].FillAmount;
    }


    public int GetItemBucketCap(int ItemBucketIndex = 0)
    {
        return ItemBuckets[ItemBucketIndex].ItemCap;
    }

    public int GetItemAmount(Item ItemToFind, int ItemBucketIndex = 0)
    {
        int amount = 0;
        ItemBuckets[ItemBucketIndex] = ItemBuckets[ItemBucketIndex].GetBucketItemCount(ItemToFind, out amount);
        return amount;
    }

    private void Awake()
    {

        playerRB = gameObject.GetComponent<Rigidbody>();
        foreach (var item in ResourceBuckets)
        {
            ResourceBuckets_Dict.Add(item.ItemResource, new ItemBucket(item.Cap));
        }
    }

    public bool CanAddItem(int BucketIndex, Item itemToAdd)
    {
        return ItemBuckets[BucketIndex].FillAmount + itemToAdd.ChunkSize <= ItemBuckets[BucketIndex].ItemCap;
    }

    public bool CanAddResource(Resource resource, Item itemToAdd)
    {
        return ResourceBuckets_Dict[resource].FillAmount + itemToAdd.ChunkSize <= ResourceBuckets_Dict[resource].ItemCap;
    }


    public bool AddToItemBucket(Item itemToAdd, int amount = 1, int BucketIndex = 0)
    {
        bool success = false;
        ItemBuckets[BucketIndex] = ItemBuckets[BucketIndex].AddBucketItem(itemToAdd, out success, amount);
        playerRB.mass += InventoryMassMultiplier * itemToAdd.Mass * amount;
        CarriedItemMassRaw += itemToAdd.Mass * amount;
        Debug.Log(""+CarriedItemMassRaw );
        return success;
    }

    public bool RemoveFromItemBucket(Item itemToRemove, int amount = 1, int BucketIndex = 0)
    {
        bool success = false;
        ItemBuckets[BucketIndex] = ItemBuckets[BucketIndex].RemoveBucketItem(itemToRemove, out success, amount);
        playerRB.mass -= InventoryMassMultiplier * itemToRemove.Mass * amount;
        CarriedItemMassRaw -= itemToRemove.Mass  * amount;
        return success;
    }

    public void OffloadSalvage(ResourceInventory TargetInventory)
    {
        List<KeyValuePair<Resource, ItemBucket>> ResourceList = ResourceBuckets_Dict.ToList();
        for (int i = 0; i < ResourceList.Count; i++)
        {
            TargetInventory.AddResource(ResourceList[i].Key, ResourceList[i].Value.FillAmount);
            ResourceBuckets_Dict[ResourceList[i].Key] = new ItemBucket(ResourceList[i].Value.ItemCap);
        }
        playerRB.mass -= CarriedResourceMassRaw * InventoryMassMultiplier;
        CarriedResourceMassRaw = 0;
        AkSoundEngine.PostEvent("Octo_Resc_Deposit", gameObject);
    }

    public Dictionary<Item,int> GetResourceSalvageList(Resource resource)
    {
        return ResourceBuckets_Dict[resource].Bucket;
    }

    public Dictionary<Item,int> GetResourceSalvageSpaceList(Resource resource)
    {
        Dictionary<Item,int> returnDict = new Dictionary<Item, int>();
        foreach (var item in GetResourceSalvageList(resource))
        {
            returnDict.Add(item.Key,item.Key.Size*item.Value);
        };
        return returnDict;
    }


    public bool CanAddToResourceBucket(Item itemToAdd, int amount = 1)
    {
        return ResourceBuckets_Dict[itemToAdd.ResourceType].FillAmount + (itemToAdd.Size * amount) <= ResourceBuckets_Dict[itemToAdd.ResourceType].ItemCap;
    }

    public bool AddToResourceBucket(Item itemToAdd, int amount = 1)
    {
        bool success = false;

        //update octo's carry weight modifier
        totalInvWeight += (itemToAdd.ChunkSize * amount);
        player.GetComponent<MovementController>().SetInventoryWeight(totalInvWeight);
        //Debug.LogWarning(totalInvWeight);

        //Debug.Log("Test");
        //Debug.Log(amount);
        ResourceBuckets_Dict[itemToAdd.ResourceType] = ResourceBuckets_Dict[itemToAdd.ResourceType].AddBucketItem(itemToAdd, out success, amount);
        CarriedResourceMassRaw += itemToAdd.Mass* amount;
        playerRB.mass += InventoryMassMultiplier * itemToAdd.Mass* amount;

        return success;
    }
    public bool RemoveFromResourceBucket(Item itemToRemove, int amount = 1)
    {
        bool success = false;

        //update octo's carry weight modifier
        totalInvWeight -= (itemToRemove.ChunkSize * amount);
        player.GetComponent<MovementController>().SetInventoryWeight(totalInvWeight);
        //Debug.LogWarning(totalInvWeight);

        ResourceBuckets_Dict[itemToRemove.ResourceType] = ResourceBuckets_Dict[itemToRemove.ResourceType].RemoveBucketItem(itemToRemove, out success, amount);
        CarriedResourceMassRaw -= itemToRemove.Mass* amount;
        playerRB.mass -= InventoryMassMultiplier * itemToRemove.Mass* amount;

        return success;
    }
}
