using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    float hitbackforce = 15.0f;
    Vector3 hitbackMoving;
    public GameObject hitUI;
    float hitreduce = 0;
    //respwan
    Vector3 startingPOS;
    bool respawning = false;
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
            hitreduce = 255;
            HealthManager.instance.ChangeHealth(20);
            //float weaker = 2.0f-(HealthManager.instance.getHP()/100);
            float weaker = 1+ (((101-HealthManager.instance.getHP())/100)*1.5f);
            hitbackMoving += (collision.gameObject.GetComponent<snowball>().getdir() * hitbackforce * weaker);           
            //hitbackMoving += (GetComponent<Transform>().position - collision.gameObject.transform.position).normalized * hitbackforce;
            //hitbackMoving += (GetComponent<Transform>().position - collision.gameObject.transform.position).normalized * hitbackforce;
            hitbackMoving.y = 0f;
            
        }
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = normalSpeed;
        startingPOS = this.gameObject.transform.position;
        Debug.Log(startingPOS);
    }
    // Update is called once per frame
    void Update()
    {
        //player moving
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");
        Vector3 move = transform.forward * forward + transform.right * right;
        if(shooting.rooted == false&& playercontrol())
            controller.Move(move * speed * Time.deltaTime);

        //is ground check
        isground = Physics.CheckSphere(groundcheck.position, 0.4f, ground);

        //downforce
        velocity.y += downforce*Time.deltaTime;

        //jump
        if (Input.GetButtonDown("Jump") == true && isground == true &&playercontrol())
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
        //hit ui reduce
        hitUI.GetComponent<Image>().color = new Color32(255, 255, 225, (byte)hitreduce);
        hitreduce = Mathf.Lerp(hitreduce, 0, Time.deltaTime);
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


        
        
    }
    private void FixedUpdate()
    {
        //fall out reset
        if (gameObject.transform.position.y < -6 && !respawning)
        {
            respawning = true;
            this.gameObject.transform.position = startingPOS;
            teamscore.instance.score4team2(1);
            HealthManager.instance.refillHP();
            gameObject.GetComponent<shooting>().resetweapon();
            hitbackMoving = Vector3.zero;
            respawning = false;
        }
    }
    bool playercontrol()
    {
        if (!PauseMenu.ingaming || PauseMenu.inpause)
            return false;
        return true;
    }

}
