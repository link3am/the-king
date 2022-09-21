using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float downforce = -9.8f;
    public LayerMask ground;
    public Transform groundcheck;
    public float jumpforce = 7f;
    
    public bool isground;
    Vector3 velocity;
    bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //player moving
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");
        Vector3 move = transform.forward * forward + transform.right * right;
        controller.Move(move * speed * Time.deltaTime);



        //is ground check
        isground = Physics.CheckSphere(groundcheck.position, 0.6f, ground);


        
        //downforce
        velocity.y += downforce*Time.deltaTime;

        //jump
        jump = Input.GetButtonDown("Jump");
        if (jump == true && isground == true)
        {
            velocity.y += jumpforce;
        }
        if (isground && velocity.y < 0)
        { velocity.y = -2f; }
        
        
        //

        controller.Move(velocity * Time.deltaTime);
    }
}
