using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shooting : MonoBehaviour
{

    public Camera playerCam;
    public GameObject gunPoint;
    public GameObject bullet1;
    public float bulletforce = 25;
    public int ammo = 3;
    public Text ammoDisplay;
    public Text ammoingHint;
    public Text fillammo;
    public bool inAmmoPoint = false;
    //Vector3 diraction;
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("storm"))
        {
            //Debug.Log("in");
            inAmmoPoint = true;
            fillammo.text = "Press F key to fill snowball";
        }
      

     }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("storm"))
        {
            //Debug.Log("out");
            inAmmoPoint = false;
            fillammo.text = "";
        }
    }

 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
       
        if (Input.GetButtonDown("Fire1") && ammo > 0 && !PauseMenu.isGamePaused && !PauseMenu.isGameOver)
        {
            shoot();
            ammo -= 1;
            SoundManager.PlaySound(SoundManager.SoundFX.PlayerShoot);
        }
        if (Input.GetButtonDown("Fire2") && inAmmoPoint == true && !PauseMenu.isGamePaused && !PauseMenu.isGameOver)
        {
            ammo++;
        }

        //ammoDisplay.text = "ammo: " + ammo.ToString();
        ammoDisplay.text = ammo.ToString();
        if (ammo < 4&& !inAmmoPoint)
            ammoingHint.text = "Stay snowing point to refill snowgun!";
        else
            ammoingHint.text = "";


    }
   //private void LateUpdate()
   //{
   //    inAmmoPoint = false;
   //    fillammo.text = "";
   //}
    void shoot()
    {
        //GameObject newbullet = Instantiate(bullet1, gunPoint.transform.position, Quaternion.identity);
        GameObject newbullet = objectPooler.instance.getFromPool("bullet", gunPoint.transform.position, Quaternion.identity);

        //for(int i=0; i<20; i++)
        //{
        //    GameObject garbage = Instantiate(bullet1, new Vector3(0,-5,0), Quaternion.identity);
        //    garbage.GetComponent<snowball>().setshooter(this.gameObject, playerCam.transform.forward);
        //}

        newbullet.GetComponent<snowball>().setshooter(this.gameObject, playerCam.transform.forward);
        //RaycastHit aimed;
        //if(Physics.Raycast(playerCam.transform.position,playerCam.transform.forward,out aimed))
        //{
        //    //Debug.Log(aimed.transform.name);
        //    Vector3 targetDir = (aimed.point - gun.transform.position).normalized;
        //    newbullet.GetComponent<Rigidbody>().AddForce(targetDir * bulletforce, ForceMode.Impulse);
        //
        //
        //}
        //else


        //newbullet.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * bulletforce, ForceMode.Impulse);
        Rigidbody rb = newbullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(playerCam.transform.forward * bulletforce, ForceMode.Impulse);


    }
}
