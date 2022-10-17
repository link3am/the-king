using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    CharacterController controller;

    Vector3 velocity;
    public float snowballforce = 20f;
    Vector3 hitbackMoving;
    float timer;
    public GameObject bullet1;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("snowball"))
        {
            hitbackMoving += collision.gameObject.GetComponent<snowball>().getshooter()* snowballforce;
            //hitbackMoving += (GetComponent<Transform>().position - collision.gameObject.transform.position).normalized * snowballforce;
            hitbackMoving.y = 0f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //shooting test
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //hitback reduce
        if (hitbackMoving.magnitude > 0.2f)
        {
            controller.Move(hitbackMoving * Time.deltaTime);
        }
        hitbackMoving = Vector3.Lerp(hitbackMoving, Vector3.zero, 3 * Time.deltaTime);
        //

        controller.Move(Vector3.down * 2);
        //hitback reset
        controller.Move(velocity * Time.deltaTime);


       
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            shoot();
            timer = 0f;
        }

    }
    void shoot()
    {
        GameObject newbullet = Instantiate(bullet1, this.transform.position + new Vector3(0,0,-1), Quaternion.identity);
        newbullet.GetComponent<snowball>().setshooter(this.gameObject,Vector3.back);
        
        newbullet.GetComponent<Rigidbody>().AddForce(Vector3.back * 20, ForceMode.Impulse);



    }
}
