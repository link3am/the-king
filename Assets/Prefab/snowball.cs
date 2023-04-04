using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowball : MonoBehaviour
{
    //GameObject shooter;
    Vector3 dir;
    Rigidbody rb;
    GameObject shooter;
    public GameObject particle;
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), shooter.GetComponent<Collider>(), false);

        
        GameObject temp = Instantiate(particle, gameObject.transform.position, Quaternion.identity);
        temp.GetComponent<ParticleSystem>().Play();
        //Destroy(this.gameObject);
    }

    void Start()
    {
        shooter = GameObject.FindWithTag("enemy");
        //GameObject[] snowballs = GameObject.FindGameObjectsWithTag("snowball1");
        //foreach (GameObject aa in snowballs)
        //{
        //    Physics.IgnoreCollision(this.GetComponent<Collider>(), aa.GetComponent<Collider>());
        //}

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
    public void setshooter( Vector3 bb)
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), shooter.GetComponent<Collider>());
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
