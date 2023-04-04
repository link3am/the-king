using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class look : MonoBehaviour
{
    public float mouseSpeeding;
    public Transform player;
    float xRo = 0f;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseSpeeding = slider.value;
        float mousex = Input.GetAxis("Mouse X") * mouseSpeeding * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * mouseSpeeding * Time.deltaTime;
        xRo -= mousey;
        xRo = Mathf.Clamp(xRo, -90f, 90f);
        

        if (PauseMenu.ingaming&&!PauseMenu.inpause)
        {
            player.Rotate(Vector3.up * mousex);
            transform.localRotation = Quaternion.Euler(xRo, 0, 0);
        }
    }
}
