using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    Vector3 pos;
    bool reseting;
    float timer;
    float resetTime;   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            gameObject.transform.position = new Vector3(0, 50, 0);
 
            reseting = true;

        }
    }
    void Start()
    {
       pos = gameObject.transform.position;
        resetTime = 5.0f;
        timer = 0.0f;
        reseting = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 250.0f, 0) * Time.deltaTime);
        if(reseting ==true)
        {
            timer += Time.deltaTime;
            if(timer>=resetTime)
            {
                timer = 0.0f;
                reseting = false;
                gameObject.transform.position = pos;                
            }
        }
    }
}
