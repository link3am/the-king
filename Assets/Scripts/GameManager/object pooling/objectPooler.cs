using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{
    public static objectPooler instance;
    public List<pool> pools;

    Queue<GameObject> objectpool;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (pool pool in pools)
        {
            objectpool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.fab);         
                obj.SetActive(false);
                objectpool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectpool);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject getFromPool(string tag, Vector3 pos, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning(tag + " does not exist !");
            return null;
        }
        GameObject returnobject = poolDictionary[tag].Dequeue();
        returnobject.SetActive(true);
        returnobject.transform.position = pos;
        returnobject.transform.rotation = rotation;
        
        poolDictionary[tag].Enqueue(returnobject);

        return returnobject;
    }
}
