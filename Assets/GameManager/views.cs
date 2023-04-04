using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class views : MonoBehaviour
{
    public GameObject player;
    public GameObject watcher;
    bool ishost;
    Camera playerCam;
    // Start is called before the first frame update
    void Start()
    {
        watcher.SetActive(true);
        playerCam = player.GetComponent<Camera>();
        playerCam.enabled = false;

        MPhoster script = this.gameObject.GetComponent<MPhoster>();
        if (script.enabled ==true)
        {
            ishost = true;
        }
        else
            ishost = false;
    }

    // Update is called once per frame
    void Update()
    {
        //camswitch();
        gamestart();
    }

    void camswitch()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            watcher.SetActive(true);
            playerCam.enabled = false;
            PauseMenu.ingaming = false;
            PauseMenu.waiting = true;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            watcher.SetActive(false);
            playerCam.enabled = true;
            PauseMenu.ingaming = true;
            PauseMenu.waiting = false;
        }
    }
    public void gamestart()
    {
       if(MPhoster.readyPlayer||!ishost)
        {
            watcher.SetActive(false);
            playerCam.enabled = true;
            PauseMenu.ingaming = true;
            PauseMenu.waiting = false;
        }
    }
}
