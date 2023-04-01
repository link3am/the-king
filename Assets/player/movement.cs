using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

     CharacterController controller;
   
    public float speed; //up to 18
    public float normalSpeed = 10f;
    public float upSpeed = 18f;
    //speed up
    bool speedup = false;
    float speedUPtimer = 0f;
    float speedUPlimt = 2.0f;
    //
    public float downforce = -9.8f;
    public LayerMask ground;
    public Transform groundcheck;
    public float jumpforce = 7f;
    
    public static bool isground;
    Vector3 velocity;
    bool jump = false;

    public float hitbackforce = 20f;
    Vector3 hitbackMoving;

    //
    public GameObject enemy;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("itemspd"))
        {
            speedup = true;
            speed = upSpeed;
        }
    }
       
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("snowball"))
        {
            HealthManager.instance.ChangeHealth(10);
            hitbackMoving += (collision.gameObject.GetComponent<snowball>().getdir() * hitbackforce);
            
            //hitbackMoving += (GetComponent<Transform>().position - collision.gameObject.transform.position).normalized * hitbackforce;
            //hitbackMoving += (GetComponent<Transform>().position - collision.gameObject.transform.position).normalized * hitbackforce;
            hitbackMoving.y = 0f;
            
        }
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = normalSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        //player moving
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");
        Vector3 move = transform.forward * forward + transform.right * right;
        
        if(shooting.rooted == false)
            controller.Move(move * speed * Time.deltaTime);

        //is ground check
        isground = Physics.CheckSphere(groundcheck.position, 0.4f, ground);

        //downforce
        velocity.y += downforce*Time.deltaTime;

        //jump
        jump = Input.GetButtonDown("Jump");
        if (jump == true && isground == true )
        {
            velocity.y += jumpforce;
        }
        if (isground && velocity.y < 0)
        { velocity.y = -2f; }

        //hitback reduce       
        if (hitbackMoving.magnitude > 0.2f)
        {
           controller.Move(hitbackMoving * Time.deltaTime);
        }
        hitbackMoving = Vector3.Lerp(hitbackMoving, Vector3.zero, 3 * Time.deltaTime);
        //
        //speed up item dur
        if(speedup)
        {
            speedUPtimer += Time.deltaTime;
            if(speedUPtimer > speedUPlimt)
            {
                speedup = false;
                speed = normalSpeed;
            }
        }

        //apply jump and hitback
        controller.Move(velocity * Time.deltaTime);

        if(gameObject.transform.position.y < -6)
        {
            PauseMenu.isGameOver = true;
        }

        

        //testing tp
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    controller.enabled = false;
        //    gameObject.transform.position = new Vector3(0, 1.5f, -27.2999992f);
        //    controller.enabled = true;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    controller.enabled = false;
        //    gameObject.transform.position = new Vector3(0, 1.5f, 2.0999999f);
        //    controller.enabled = true;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    controller.enabled = false;
        //    gameObject.transform.position = new Vector3(-46f, -1.89999998f, 9.39999962f);
        //    controller.enabled = true;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    controller.enabled = false;
        //    gameObject.transform.position = new Vector3(-53.7000008f, 1.5f, -1.10000002f);
        //    controller.enabled = true;
        //}
    }
}
