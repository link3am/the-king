using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunswaing : MonoBehaviour
{
    public float amount; //swaing movement
    public float amount2; //swaing speed
    public float maxX; //movement range
    public float maxY;

    Vector3 startPos;




    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.ingaming && !PauseMenu.inpause)
        {
            float moveX = -Input.GetAxis("Mouse X") * amount;
            float moveY = -Input.GetAxis("Mouse Y") * amount;
            moveX = Mathf.Clamp(moveX, -maxX, maxX);
            moveY = Mathf.Clamp(moveY, -maxY, maxY);
            Vector3 endPos = new Vector3(moveX, moveY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, endPos + startPos, amount2 * Time.deltaTime);
            float forward = Input.GetAxis("Vertical");
            float right = Input.GetAxis("Horizontal");
            Vector3 moving = new Vector3(-right, 0, -forward) * 0.2f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, moving + startPos, amount2 * Time.deltaTime);
        }
    }
}
