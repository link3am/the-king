using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowball : MonoBehaviour
{
    GameObject shooter;
    Vector3 dir;
    private void OnCollisionEnter(Collision collision)
    {
       

        Destroy(this.gameObject);
      
     
    }
    public void setshooter(GameObject aa,Vector3 bb)
    {
        shooter = aa;
        dir = bb.normalized;
    }
    public Vector3 getdir()
    {
        return dir;
    }
   
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), shooter.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    
}
