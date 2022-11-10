using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowpoint : MonoBehaviour
{
    public float pointLife;
    float timer;
    

   
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= pointLife )
            gameObject.transform.position = new Vector3(0, -50, 0);

        if (timer > pointLife + 1)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            timer = 0.0f;
        }

    }


    //public Transform trans;
    //public  GameObject snowArea;
    //float xPos;
    //float zPos;
    ////float height = 200;
    //float timer = 0;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    xPos = Random.Range(25, -25);
    //    zPos = Random.Range(25, -25);
    //
    //    if (Physics.Raycast(new Vector3(xPos, 200, zPos), Vector3.down, out RaycastHit hit))
    //        trans.position.Set(xPos, hit.point.y, zPos);
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if(timer >= 3.0)
    //    {
    //        xPos = Random.Range(25, -25);
    //        zPos = Random.Range(25, -25);
    //
    //        if (Physics.Raycast(new Vector3(xPos, 200, zPos), Vector3.down, out RaycastHit hit))
    //            trans.position.Set(xPos, hit.point.y, zPos);
    //
    //        timer -=3;
    //    }
    //}
}
