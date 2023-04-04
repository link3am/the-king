using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
public class MainMenu : MonoBehaviour
{
    public GameObject p1;//main
    public GameObject p2;//help
    public GameObject p3;//host or client
    public GameObject p4;//as host
    public GameObject p5;//as client

    public GameObject de1;
    public GameObject de2;

    public GameObject runhost;
    public GameObject runclient;
    public GameObject iptext;
    public InputField ipenter;

    private void Start()
    {
        p1.SetActive(true);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
        p5.SetActive(false);
        //botton
        iptext.GetComponent<Text>().text = ("");
    }
    public void startgame()
    {
        SceneManager.LoadScene(1);
    }
    public void quitgame()
    {
        Application.Quit();
    }  
    public void gomainPage()
    {
        p1.SetActive(true);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
        p5.SetActive(false);
    }
    public void gohelpPage()
    {
        p1.SetActive(false);
        p2.SetActive(true);
        p3.SetActive(false);
        p4.SetActive(false);
        p5.SetActive(false);
    }
    public void golevelPage()
    {
        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(true);
        p4.SetActive(false);
        p5.SetActive(false);

    }

    public void gohost()
    {
        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(true);
        p5.SetActive(false);

        string temp = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
        //reference https://stackoverflow.com/questions/8709427/get-local-system-ip-address-using-c-sharp
        iptext.GetComponent<Text>().text = ("Host IP: \n" + temp);
    }
    public void goclient()
    {
        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
        p5.SetActive(true);
    }
    public void startD1()
    {
        SceneManager.LoadScene(3);
    }
    public void startD2()
    {

    }
    public void joingame()
    {
        string hostIP = ipenter.text;
    }
}
