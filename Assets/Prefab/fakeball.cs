using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakeball : MonoBehaviour
{
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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setshooter(GameObject aa)
    {
        shooter = aa;
        Physics.IgnoreCollision(this.GetComponent<Collider>(), aa.GetComponent<Collider>());

    }
}
