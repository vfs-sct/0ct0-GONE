using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Pooling/New Pool")]
public class ObjectPool : ScriptableObject
{

    [SerializeField] public GameObject PooledObject; //supports any object, NOTE: instanciating classes will ONLY use the default constructor!!!!
                                                    //TODO: Make poolable object subclass to allow for custom constructors
    [SerializeField] public int PoolSize;
    [SerializeField] private int CheckDistance = 3;
    [SerializeField] private bool FixedSize = false;

    private Queue<GameObject> ActivePool = new Queue<GameObject>();
    private Stack<GameObject> InActivePool = new Stack<GameObject>();

    private HashSet<GameObject> AllObjects = new HashSet<GameObject>();

    private GameObject temp;

    private int curCount;
    protected void OnEnable()
    {
        ActivePool.Clear();
        InActivePool.Clear();
        AllObjects.Clear();

    }

    private void InstantiateObject() //this initializes an object of the pooled type, uses reflection to initialize
    {
        temp = GameObject.Instantiate(PooledObject);
        temp.SetActive(false);
        InActivePool.Push(temp);
        AllObjects.Add(temp);
    }

    public virtual void InitializePool()
    {
        for (int i = 0; i < PoolSize; i++){InstantiateObject();} //oh baby this is CLEEEEAAAAN!
    }


    //Activates and returns an object off the inactive Stack, warning unchecked cast. Improper use results in dead kittens!
    public GameObject GetObject()
    {
        //TODO Implement cycling of raw Objects
        if (InActivePool.Count == 0) if (!FixedSize){InstantiateObject();}else {throw new System.Exception(this +": uses fixed size. Error: Object Pool Empty");};
        curCount = 0;
        while (curCount <CheckDistance && ActivePool.Count > 0)
            {
               temp = ActivePool.Dequeue();
                if (temp.activeSelf){ActivePool.Enqueue(temp);temp.SetActive(true);} else {InActivePool.Push(temp);};
                curCount++;
            }
        temp = InActivePool.Pop();
        ActivePool.Enqueue(temp);
        temp.SetActive(true);
        
        return temp;
    }

}
