using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDetect : MonoBehaviour
{
    public GameObject HitScreen;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "snowball1" || collision.gameObject.tag == "snowball2")
        {
            GotHit();
        }
    }

    private void GotHit()
    {
        var colour = HitScreen.GetComponent<Image>().color;
        colour.a = 0.8f;
        HitScreen.GetComponent<Image>().color = colour;
    }

    private void Update()
    {
        if (HitScreen != null)
        {
            if (HitScreen.GetComponent<Image>().color.a > 0)
            {
                var colour = HitScreen.GetComponent<Image>().color;

                colour.a -= 0.001f;

                HitScreen.GetComponent<Image>().color = colour;
            }
        }
    }
}
