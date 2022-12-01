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
    int level = 1;
    public GameObject bullet1;

    Subject subject = new Subject();

    GameObject findplayer;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("snowball"))
        {
            hitbackMoving += collision.gameObject.GetComponent<snowball>().getdir()* snowballforce;
            //hitbackMoving += (GetComponent<Transform>().position - collision.gameObject.transform.position).normalized * snowballforce;
            hitbackMoving.y = 0f;
        }      
    }
    private void OnEnable()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //shooting test
        timer = 0f;
        findplayer = GameObject.FindGameObjectWithTag("player");

        //observer
        Death death = new Death(this.gameObject);
        subject.AddObserver(death);
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
        //gravity
        controller.Move(Vector3.down * 5 * Time.deltaTime);
        //hitback reset
        controller.Move(velocity * Time.deltaTime);

        //face player
        gameObject.transform.LookAt(findplayer.transform);
        gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);

        timer += Time.deltaTime;
        if (timer > level)
        {
            shoot();
            timer = 0f;
        }



        //fall out death check
        deathcheck();

    }
    void shoot()
    {
        GameObject newbullet = objectPooler.instance.getFromPool("bullet", this.transform.position, Quaternion.identity);

        newbullet.GetComponent<snowball>().setshooter(this.gameObject, aimplayer());
        Rigidbody rb = newbullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(aimplayer() * 20, ForceMode.Impulse);

    }
    Vector3 aimplayer()
    {
        
        return (findplayer.transform.position - this.transform.position).normalized;
        
    }
    //for redom spawn mode
    //public void setenemy(int lv,Subject subject)
    //{
    //    level = 6-lv;
    //    //Debug.Log("level " + lv + " enemy set");
    //    this.subject = subject;
    //}
    void deathcheck()
    {
        if (transform.position.y < -6)
        {
           //observer call
            subject.Notify(this.gameObject);

            timer = 0f;
            Destroy(this.gameObject);
        }
    }
}
