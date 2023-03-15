using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class look : MonoBehaviour
{
    public float mouseSpeeding = 10f;
    public Transform player;
    float xRo = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxis("Mouse X") * mouseSpeeding * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * mouseSpeeding * Time.deltaTime;
        xRo -= mousey;
        xRo = Mathf.Clamp(xRo, -90f, 90f);
        player.Rotate(Vector3.up * mousex);

        transform.localRotation = Quaternion.Euler(xRo, 0, 0);
    }
}
