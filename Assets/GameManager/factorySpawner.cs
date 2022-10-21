using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class factorySpawner : MonoBehaviour
{

    float xPos;
    float zPos;
    public float spawnTime;
    float timer;

    public GameObject fab;
    public GameObject enemy;
    
    GameObject temp;

    Subject subject = new Subject();
    // Start is called before the first frame update
    void Start()
    {
        Death death = new Death(this.gameObject);
        subject.AddObserver(death);
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

            //enemt spawn
            timer -= spawnTime;
            xPos = Random.Range(25, -25);
            zPos = Random.Range(25, -25);
            temp = Instantiate(enemy, new Vector3(xPos, 4, zPos), transform.rotation);
           
            
            temp.GetComponent<enemy>().setenemy(Random.Range(1, 5), subject);
        }

      
       

    }
 
}
