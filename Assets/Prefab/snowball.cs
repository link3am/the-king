using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowball : MonoBehaviour
{
    //GameObject shooter;
    Vector3 dir;
    Rigidbody rb;
    GameObject shooter;
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(this.gameObject);
        bulletOut();
    }


    void Start()
    {
    }
   void Update()
    {
        if (transform.position.y < -10)
        {
            //Destroy(this.gameObject);
            bulletOut();
        }
    }



    public void setshooter(GameObject aa,Vector3 bb)
    {
        shooter = aa;
        Physics.IgnoreCollision(this.GetComponent<Collider>(), aa.GetComponent<Collider>());
        dir = bb.normalized;
    }
    public Vector3 getdir()
    {
        return dir;
    }
   
    void bulletOut()
    {
        gameObject.SetActive(false);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), shooter.GetComponent<Collider>(),false);
    }

    
}
