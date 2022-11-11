using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class factorySpawner : MonoBehaviour
{

    float xPos;
    float zPos;
    public float spawnTime1;
    public float spawnTime2;
    float timer1;
    float timer2;

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
        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer1 > spawnTime1)
        {
            //snowpoint spawn
            timer1 -= spawnTime1;
            xPos = Random.Range(-20, 20);
            zPos = Random.Range(-20, 20);
            //Instantiate(fab, new Vector3(xPos, 2, zPos), transform.rotation);
            objectPooler.instance.getFromPool("snowpoint", new Vector3(xPos, 2, zPos), Quaternion.identity);
            //Debug.Log(xPos);
            //Debug.Log(zPos);

            float xx = Random.Range(-20, 20);
            float zz = Random.Range(-20, 20);
            temp = Instantiate(enemy, new Vector3(xPos, 2, zPos), transform.rotation);
            temp.GetComponent<enemy>().setenemy(Random.Range(1, 5), subject);
            //objectPooler.instance.getFromPool("enemy", new Vector3(xx, 2, zz), Quaternion.identity);
        }
        //if(timer2 > spawnTime2)
        //{
        //    //enemt spawn
        //    timer2 -= spawnTime2;
        //    //xPos = Random.Range(-20, 20);
        //    //zPos = Random.Range(-20, 20);
        //    temp = Instantiate(enemy, new Vector3(xPos, 2, zPos), transform.rotation);
        //    //objectPooler.instance.getFromPool("enemy", new Vector3(xPos, 2, zPos), Quaternion.identity);
        //
        //    //temp.GetComponent<enemy>().setenemy(Random.Range(1, 5), subject);
        //}

    
       

    }
 
}
