using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowpointSpawner : MonoBehaviour
{

    float xPos;
    float zPos;
    public float spawnTime;
    float timer;

    public GameObject fab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=spawnTime)
        {
            timer -= spawnTime;
            xPos = Random.Range(25, -25);
            zPos = Random.Range(25, -25);
            Instantiate(fab, new Vector3(xPos, 2, zPos), transform.rotation);
        }

    }
}
