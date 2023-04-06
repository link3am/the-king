using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using TMPro;
public class views : MonoBehaviour
{
    public GameObject player;
    public GameObject watcher;
    public GameObject waitingUI;
    public GameObject endUI;
    public GameObject endtext;
    bool ishost;
    Camera playerCam;

    public PlayableDirector waitcut;
    public PlayableDirector endcut;
    public GameObject endcam;
    public GameObject p1;
    public GameObject p2;
    // Start is called before the first frame update
    void Start()
    {
        watcher.SetActive(true);
        waitingUI.SetActive(true);
        playerCam = player.GetComponent<Camera>();
        playerCam.enabled = false;

        MPhoster script = this.gameObject.GetComponent<MPhoster>();
        if (script.enabled == true)
        {
            ishost = true;
            waitcut.Play();
        }
        else
        {
            ishost = false;
            watcher.SetActive(false);
            waitingUI.SetActive(false);
            playerCam.enabled = true;
            PauseMenu.ingaming = true;
           
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (MPhoster.readyPlayer)
        {
            waitcut.Stop();
            watcher.SetActive(false);
            waitingUI.SetActive(false);
            playerCam.enabled = true;
            PauseMenu.ingaming = true;
            
        }
        if(PauseMenu.p1win)
        {
            PauseMenu.ingaming = false;
            watcher.SetActive(true);
            endUI.SetActive(true);
            endtext.GetComponent<TMPro.TextMeshProUGUI>().text = "Your Win";
            playerCam.enabled = false;
            endcam.GetComponent<CinemachineVirtualCamera>().LookAt  =p1.transform;
            endcam.GetComponent<CinemachineVirtualCamera>().Follow = p1.transform;
            endcut.Play();
            if (ishost)
                MPhoster.serverclose();
            else
                MPclient.clientclose();

        }
        if (PauseMenu.p2win)
        {
            PauseMenu.ingaming = false;
            watcher.SetActive(true);
            endUI.SetActive(true);
            endtext.GetComponent<TMPro.TextMeshProUGUI>().text = "Enemy Win";
            playerCam.enabled = false;
            endcam.GetComponent<CinemachineVirtualCamera>().LookAt = p2.transform;
            endcam.GetComponent<CinemachineVirtualCamera>().Follow = p2.transform;
            endcut.Play();
            if (ishost)
                MPhoster.serverclose();
            else
                MPclient.clientclose();
        }
    }

    //void camswitch()
    //{
    //    if(Input.GetKeyDown(KeyCode.F2))
    //    {
    //        watcher.SetActive(true);
    //        playerCam.enabled = false;
    //        PauseMenu.ingaming = false;
    //        PauseMenu.waiting = true;
    //    }
    //    if (Input.GetKeyDown(KeyCode.F3))
    //    {
    //        watcher.SetActive(false);
    //        playerCam.enabled = true;
    //        PauseMenu.ingaming = true;
    //        PauseMenu.waiting = false;
    //    }
    //}
    //public void gamestart()
    //{
    //    
    //    if (MPhoster.readyPlayer || !ishost)
    //    {
    //        waitcut.Stop();
    //        watcher.SetActive(false);
    //        playerCam.enabled = true;
    //        PauseMenu.ingaming = true;
    //    }
    //    
    //}
    //public void gameover()
    //{
    //    if (PauseMenu.p1win || PauseMenu.p2win)
    //    {
    //        watcher.SetActive(true);
    //        playerCam.enabled = false;
    //        PauseMenu.ingaming = false;
    //        endcut.Play();
    //    }
    //}
}
