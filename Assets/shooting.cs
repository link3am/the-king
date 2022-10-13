using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{

    public Camera playerCam;
    public GameObject gun;
    public GameObject bullet1;
    public float bulletforce = 25;
    public int ammo = 3;
    public bool inAmmoPoint = false;
    //Vector3 diraction;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("storm"))
        {
            Debug.Log("in");
            inAmmoPoint = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("storm"))
        {
            Debug.Log("out");
            inAmmoPoint = false;
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
    }

    void shoot()
    {
        GameObject newbullet = Instantiate(bullet1, gun.transform.position, Quaternion.identity);

        RaycastHit aimed;
        if(Physics.Raycast(playerCam.transform.position,playerCam.transform.forward,out aimed))
        {
            //Debug.Log(aimed.transform.name);
            Vector3 targetDir = (aimed.point - gun.transform.position).normalized;
            newbullet.GetComponent<Rigidbody>().AddForce(targetDir * bulletforce, ForceMode.Impulse);


        }
        else
            newbullet.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * bulletforce, ForceMode.Impulse);



    }
}
