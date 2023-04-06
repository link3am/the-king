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
    public float bulletforce = 25;
    public int ammo = 10;
    public Text ammoDisplay;
    public Text ammoingHint;
    public Text fillammo;
    public bool inAmmoPoint = false;
    public GameObject manager;
    AudioSource shootSound;
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

        shootSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.inpause && PauseMenu.ingaming)
        {
            if (currentWeapon == 1)
            {
                if (Input.GetButtonDown("Fire1") && ammo > 0 && Time.time >= nextfire)
                {
                    nextfire = Time.time + fireRate;
                    shoot();
                    ammo -= 1;
                    gunAnimator.SetTrigger("shot");
                }
                if (Input.GetButtonDown("Fire2"))
                    ammo += 3;
                weapon1.SetActive(true);
                weapon2.SetActive(false);
            }

            if (currentWeapon == 2)
            {
                weapon1.SetActive(false);
                weapon2.SetActive(true);
                if (Input.GetButtonDown("Fire1") && movement.isground == true)
                {
                    rooted = true;
                    shovelAnimator.SetBool("isDigging", true);

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) && digging == 0)
                currentWeapon = 1;
            if (Input.GetKeyDown(KeyCode.Alpha2))
                currentWeapon = 2;
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

        //end
        if (PauseMenu.p1win)
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
        }
    }
   
    void shoot()
    {
        //GameObject newbullet = Instantiate(bullet1, gunPoint.transform.position, Quaternion.identity);
        GameObject newbullet = objectPooler.instance.getFromPool("fakeball", gunPoint.transform.position, Quaternion.identity);

        newbullet.GetComponent<fakeball>().setshooter(this.gameObject);

        Rigidbody rb = newbullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(playerCam.transform.forward * bulletforce, ForceMode.Impulse);

        shootSound.Play();


        MPhoster script = manager.GetComponent<MPhoster>();
        if (script.enabled ==true)
        {
            MPhoster.sendbullet1(playerCam.transform.forward, gunPoint.transform.position);
        }
        else
            MPclient.sendbullet2(playerCam.transform.forward, gunPoint.transform.position);

    }
    IEnumerator rofUP()
    {
        fireRate = 0.1f;
        yield return new WaitForSeconds(5);
        fireRate = 1.0f;
    }

    public void resetweapon()
    {
        currentWeapon = 2;

        ammo = 10;
    }
}
