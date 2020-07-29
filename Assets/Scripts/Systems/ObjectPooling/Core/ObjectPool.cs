using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Systems/Pooling/New Pool")]
public class ObjectPool : ScriptableObject
{

    [SerializeField] public Object PooledObject; //supports any object, NOTE: instanciating classes will ONLY use the default constructor!!!!
                                                    //TODO: Make poolable object subclass to allow for custom constructors
    [SerializeField] public int PoolSize;
    [SerializeField] private int CheckDistance = 3;
    [SerializeField] private bool FixedSize = false;

    private static System.Type GameObjectType;

    private GameObject PooledGameObject;
    private System.Type ObjectType;
    private Queue<object> ActivePool = new Queue<object>();
    private Stack<object> InActivePool = new Stack<object>();

    private HashSet<object> AllObjects = new HashSet<object>();


    protected void OnEnable()
    {
        ActivePool.Clear();
        InActivePool.Clear();
        AllObjects.Clear();


        ObjectType = PooledObject.GetType();
        if (ObjectType == typeof(GameObject))
        {
            PooledGameObject = (GameObject)PooledObject;
        } 
        else
        {
            PooledGameObject = null;
        }

    }

    private void InstantiateObject() //this initializes an object of the pooled type, uses reflection to initialize
    {
        
        if (PooledGameObject != null) 
        {
            Object temp;
            temp = GameObject.Instantiate(PooledGameObject);
            ((GameObject)temp).SetActive(false);
            InActivePool.Push(temp);
            AllObjects.Add(temp);
        }
        else
        {
            object temp;
            temp = System.Activator.CreateInstance(ObjectType);
            InActivePool.Push(temp);
            AllObjects.Add(temp);
        }


    }

    public virtual void InitializePool()
    {
        for (int i = 0; i < PoolSize; i++){InstantiateObject();} //oh baby this is CLEEEEAAAAN!
    }


    //Activates and returns an object off the inactive Stack, warning unchecked cast. Improper use results in dead kittens!
    public T GetObject<T>() where T: Object 
    {
        //TODO Implement cycling of raw Objects

        int curCount= 0;
        GameObject temp;
        if (PooledGameObject != null)
        {
            while (curCount <CheckDistance && ActivePool.Count > 0)
            {
               temp=  ActivePool.Dequeue() as GameObject;
                if (temp.activeSelf){ActivePool.Enqueue(temp);} else {InActivePool.Push(temp);};
                curCount++;
            }
        }


        //if the inactive stack is empty, grow the stack if enabled or throw an error
        if (InActivePool.Count == 0) if (!FixedSize){InstantiateObject();}else {throw new System.Exception(this +": uses fixed size. Error: Object Pool Empty");};
        if (PooledGameObject != null)
        {
            temp = (GameObject)InActivePool.Pop();
            ActivePool.Enqueue(temp);
            temp.SetActive(true);
            return temp as T;
        }
        else
        { //I love generics
            //Note: This will break shit if used without cycling
            T tempT = (T)InActivePool.Pop();
            ActivePool.Enqueue(tempT);
            return tempT;
        }
    }

}
