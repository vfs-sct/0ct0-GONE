using System.Collections;
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
        public Dictionary<Item,int> Bucket;
        public int Count{get=> Bucket.Count;}

        public ItemBucket(int Cap)
        {
            ItemCap = Cap;
            Bucket = new Dictionary<Item,int>();
            FillAmount = 0;
        }
        public ItemBucket AddBucketItem(Item newItem, out bool Success,int amount = 1)
        {
            if (FillAmount+ (amount*newItem.Size) > ItemCap) 
            {
                Success = false;
                return this;
            }
            if (!Bucket.ContainsKey(newItem)) 
            {
                Bucket[newItem] = amount;
            }
            else 
            {
                Bucket[newItem] += amount;
            }
            FillAmount += (amount*newItem.Size);
            Success = true;
            return this;
        }
        public ItemBucket GetBucketItemCount(Item getItem,out int amount)
        {
            if (!Bucket.ContainsKey(getItem)) amount = -1;
            amount = Bucket[getItem];
            return this;
        }
        public ItemBucket RemoveBucketItem(Item remItem, out bool success,int amount = 1)
        {
            if (!Bucket.ContainsKey(remItem)) 
            {
                success =  false;
                return this;
            }
            if (Bucket[remItem]  > 0);
            Bucket[remItem] -= amount;
            success = true;
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


    //TODO Optimize bucket system to use dicts
    [SerializeField] private List<ItemBucket> ItemBuckets = new List<ItemBucket>();

    //this is for serialization
    [SerializeField] private List<ResourceBucketData> ResourceBuckets = new List<ResourceBucketData>();

    private Dictionary<Resource,ItemBucket> ResourceBuckets_Dict = new Dictionary<Resource, ItemBucket>();

    public int GetResourceAmount(Resource resource)
    {
        return ResourceBuckets_Dict[resource].FillAmount;
    }

    public ItemBucket GetResourceBucket(Resource resource)
    {
        ItemBucket FoundBucket = ResourceBuckets_Dict[resource];
        //Debug.Log("FOUND BUCKET" + resource.DisplayName);
        //foreach(var item in FoundBucket.Bucket)
        //{
        //    Debug.Log("CONTAINS " + item.name);
        //}
        return FoundBucket;
    }

    private void Awake()
    {
        foreach (var item in ResourceBuckets)
        {
            ResourceBuckets_Dict.Add(item.ItemResource,new ItemBucket(item.Cap));
        }
    }

    public bool CanAddItem(int BucketIndex,Item itemToAdd)
    {
        
        return ItemBuckets[BucketIndex].FillAmount + itemToAdd.Size <= ItemBuckets[BucketIndex].ItemCap;
    }

    public bool CanAddResource(Resource resource,Item itemToAdd)
    {
        return ResourceBuckets_Dict[resource].FillAmount + itemToAdd.Size <= ResourceBuckets_Dict[resource].ItemCap;
    }


    public bool AddToItemBucket(Item itemToAdd,int amount = 1 ,int BucketIndex = 0)
    {
        if (itemToAdd.IsResourceItem) return false;
        ItemBucket FoundBucket = ItemBuckets[BucketIndex];
        FoundBucket.Bucket.Add(itemToAdd);
        int NewFill = FoundBucket.FillAmount + itemToAdd.Size;
        if (NewFill > FoundBucket.ItemCap) return false;

    public bool RemoveFromItemBucket(Item itemToRemove,int amount = 1 ,int BucketIndex = 0)
    {
        bool success = false;
        ItemBuckets[BucketIndex] =  ItemBuckets[BucketIndex].RemoveBucketItem(itemToRemove,out success,amount);
        return success;
    }

    public void OffloadSalvage(ResourceInventory TargetInventory)
    {
        List<KeyValuePair<Resource,ItemBucket>> ResourceList = ResourceBuckets_Dict.ToList();
        for (int i = 0; i < ResourceList.Count; i++)
        {
            TargetInventory.AddResource(ResourceList[i].Key,ResourceList[i].Value.FillAmount);
            ResourceBuckets_Dict[ResourceList[i].Key] = new ItemBucket(ResourceList[i].Value.ItemCap);
        }
    }

    public bool AddToResourceBucket(Item itemToAdd,int amount = 1)
    {
        if (!itemToAdd.IsResourceItem) return false;
        ItemBucket FoundBucket = ResourceBuckets_Dict[resource];
        FoundBucket.Bucket.Add(itemToAdd);
        int NewFill = FoundBucket.FillAmount + itemToAdd.Size;
        if (NewFill > FoundBucket.ItemCap) return false;
        ResourceBuckets_Dict[resource] = new ItemBucket(FoundBucket,NewFill);
        return true;
    }
    public bool RemoveFromResourceBucket(Item itemToRemove, int amount = 1)
    {
        ItemBucket FoundBucket = ResourceBuckets_Dict[resource];
        if (!FoundBucket.Bucket.Contains(itemToRemove))
        {
            Debug.Log("Item Not Found");
            return false;
        }
        ResourceBuckets_Dict[resource] = new ItemBucket(FoundBucket,FoundBucket.FillAmount- itemToRemove.Size);
        FoundBucket.Bucket.Remove(itemToRemove);
        //Debug.Log("REMOVED " + itemToRemove.name);
        //Debug.Log(resource.DisplayName + " BUCKET UPDATED");
        //foreach (var item in FoundBucket.Bucket)
        //{
        //    Debug.Log("CONTAINS " + item.name);
        //}
        FoundBucket.Bucket.TrimExcess();
        return true;
    }


}
