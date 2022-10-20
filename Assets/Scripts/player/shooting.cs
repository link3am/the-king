using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shooting : MonoBehaviour
{

    public Camera playerCam;
    public GameObject gun;
    public GameObject bullet1;
    public float bulletforce = 25;
    public int ammo = 3;
    public Text ammoDisplay;
    public Text fillammo;
    public bool inAmmoPoint = false;
    //Vector3 diraction;
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("storm"))
        {
            Debug.Log("in");
            inAmmoPoint = true;
            fillammo.text = "Hold F key to fill snowball";
        }
      

     }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("storm"))
        {
            Debug.Log("out");
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
       
       
        if (Input.GetButtonDown("Fire1") && ammo > 0)
        {
            shoot();
            ammo -= 1;
        }
        if (Input.GetButtonDown("Fire2") && inAmmoPoint == true)
        {
            ammo++;
        }

        ammoDisplay.text = "ammo: " + ammo.ToString();
  
    }
   //private void LateUpdate()
   //{
   //    inAmmoPoint = false;
   //    fillammo.text = "";
   //}
    void shoot()
    {
        GameObject newbullet = Instantiate(bullet1, gun.transform.position, Quaternion.identity);
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
            newbullet.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * bulletforce, ForceMode.Impulse);



    }
}