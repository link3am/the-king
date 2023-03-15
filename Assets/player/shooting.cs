using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shooting : MonoBehaviour
{
    
    public Animator shovelAnimator;
    public Animator gunAnimator;
    public Camera playerCam;
    public GameObject gunPoint;
    public GameObject bullet1;
    public GameObject weapon1;
    public GameObject weapon2;    
    public float bulletforce = 35;
    public int ammo = 3;
    public Text ammoDisplay;
    public Text ammoingHint;
    public Text fillammo;
    public bool inAmmoPoint = false;
    float nextfire = 0;
    float fireRate = 1;

    
    
    int currentWeapon;
    public static bool rooted;
    float digging = 0;
    //Vector3 diraction;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("storm"))
        {
            //Debug.Log("in");
            inAmmoPoint = true;
            fillammo.text = "Press F key to fill snowball";
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("itemammo"))
        {
            ammo += 3;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("itempow"))
        {
            StartCoroutine(rofUP());
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
        currentWeapon = 2;
        
        shovelAnimator.SetBool("isDigging", false);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (currentWeapon == 1)
        {
            if (Input.GetButtonDown("Fire1") && ammo > 0 && !PauseMenu.isGamePaused && !PauseMenu.isGameOver && Time.time >= nextfire)
            {
                nextfire = Time.time + fireRate;
                shoot();
                ammo -= 1;
                SoundManager.PlaySound(SoundManager.SoundFX.PlayerShoot);
                gunAnimator.SetTrigger("shot");
            }
            if (Input.GetButtonDown("Fire2") && !PauseMenu.isGamePaused && !PauseMenu.isGameOver)
            {
                ammo += 3;
            }
        }
        if (currentWeapon == 1)
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
        }
        if (currentWeapon == 2)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)&&digging==0)
            currentWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentWeapon = 2;
        if (Input.GetButtonDown("Fire1") && movement.isground == true&&currentWeapon ==2)
        {
            rooted = true;
            shovelAnimator.SetBool("isDigging", true);
            
        }
        if(rooted == true)
        {
            digging += Time.deltaTime;
            if(digging>1.375f)
            {
                digging = 0;
                shovelAnimator.SetBool("isDigging", false);
                rooted = false;
                ammo += 3;
            }
        }    
       
        ammoDisplay.text = ammo.ToString();
        if (ammo < 4&& !inAmmoPoint)
            ammoingHint.text = "Use shovel to refill snowgun!";
        else
            ammoingHint.text = "";


    }
   
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
    IEnumerator rofUP()
    {
        fireRate = 0.1f;
        yield return new WaitForSeconds(5);
        fireRate = 1.0f;
    }

   
}
