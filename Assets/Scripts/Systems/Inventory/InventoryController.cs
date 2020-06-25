using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [System.Serializable]
    public struct ItemBucket
    {
        public int ItemCap;

        public int FillAmount;
        private List<Item> _Bucket;
        public List<Item> Bucket{get=>_Bucket;}
        public ItemBucket(int Cap)
        {
            ItemCap = Cap;
            _Bucket = new List<Item>();
            FillAmount = 0;
        }
        public ItemBucket(ItemBucket OldBucket,int NewFillAmount)
        {
            ItemCap = OldBucket.ItemCap;
            _Bucket = OldBucket._Bucket;
            FillAmount = NewFillAmount;
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

    [SerializeField] private List<ItemBucket> ItemBuckets = new List<ItemBucket>();

    //this is for serialization
    [SerializeField] private List<ResourceBucketData> ResourceBuckets = new List<ResourceBucketData>();

    private Dictionary<Resource,ItemBucket> ResourceBuckets_Dict = new Dictionary<Resource, ItemBucket>();

    public int GetFillAmount(Resource resource)
    {
        return ResourceBuckets_Dict[resource].FillAmount;
    }

    public ItemBucket GetResourceBucket(Resource resource)
    {
        ItemBucket FoundBucket = ResourceBuckets_Dict[resource];
        Debug.Log("FOUND BUCKET" + resource.DisplayName);
        foreach(var item in FoundBucket.Bucket)
        {
            Debug.Log("CONTAINS " + item.name);
        }
        return FoundBucket;

        //ItemBucket FoundBucket = ResourceBuckets_Dict[resource];
        //FoundBucket.Bucket.ForEach()
        //if (!FoundBucket.Bucket.Contains(itemToRemove)) return false;
        //ResourceBuckets_Dict[resource] = new ItemBucket(FoundBucket, FoundBucket.FillAmount - itemToRemove.Size);
        //FoundBucket.Bucket.Remove(itemToRemove);
        //FoundBucket.Bucket.TrimExcess();
        //return true;
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


    public bool AddToItemBucket(int BucketIndex,Item itemToAdd)
    {
        if (itemToAdd.IsResourceItem) return false;
        ItemBucket FoundBucket = ItemBuckets[BucketIndex];
        FoundBucket.Bucket.Add(itemToAdd);
        int NewFill = FoundBucket.FillAmount + itemToAdd.Size;
        if (NewFill > FoundBucket.ItemCap) return false;

        ItemBuckets[BucketIndex] = new ItemBucket(FoundBucket,NewFill);
        return true;
    }

    public bool RemoveFromItemBucket(int BucketIndex,Item itemToRemove)
    {
        ItemBucket FoundBucket = ItemBuckets[BucketIndex];
        if (!FoundBucket.Bucket.Contains(itemToRemove)) return false;
        ItemBuckets[BucketIndex] = new ItemBucket(FoundBucket,FoundBucket.FillAmount- itemToRemove.Size);
        FoundBucket.Bucket.Remove(itemToRemove);
        FoundBucket.Bucket.TrimExcess();
        return true;
    }

    public bool AddToResourceBucket(Resource resource,Item itemToAdd)
    {
        if (!itemToAdd.IsResourceItem) return false;
        ItemBucket FoundBucket = ResourceBuckets_Dict[resource];
        FoundBucket.Bucket.Add(itemToAdd);
        int NewFill = FoundBucket.FillAmount + itemToAdd.Size;
        if (NewFill > FoundBucket.ItemCap) return false;
        ResourceBuckets_Dict[resource] = new ItemBucket(FoundBucket,NewFill);
        return true;
    }
    public bool RemoveFromResourceBucket(Resource resource,Item itemToRemove)
    {
        ItemBucket FoundBucket = ResourceBuckets_Dict[resource];
        if (!FoundBucket.Bucket.Contains(itemToRemove))
        {
            Debug.Log("Item Not Found");
            return false;
        }
        ResourceBuckets_Dict[resource] = new ItemBucket(FoundBucket,FoundBucket.FillAmount- itemToRemove.Size);
        FoundBucket.Bucket.Remove(itemToRemove);
        Debug.Log("REMOVED " + itemToRemove.name);
        Debug.Log(resource.DisplayName + " BUCKET UPDATED");
        foreach (var item in FoundBucket.Bucket)
        {
            Debug.Log("CONTAINS " + item.name);
        }
        FoundBucket.Bucket.TrimExcess();
        return true;
    }


}
