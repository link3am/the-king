using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;


public class MPclient : MonoBehaviour
{

    private static Socket client;
    //static EndPoint serverEP = new IPEndPoint(IPAddress.Parse("10.0.0.126"), 8888);
    public GameObject player;
    GameObject temp;
    private static byte[] sendbuffer = new byte[512];
    private static float[] sposA;
    private static float[] sfacingA;

    //connection check
    static bool connected = false;
    float timer = 0.0f;
    float timeout = 3.0f;
    //recv
    public GameObject enemy;
    private static byte[] recvbuffer = new byte[512];
    private static float[] posA;
    private static Vector3 pos;
    private static float[] facingA;
    private static Vector3 facing;
    //bullet
    static bool trigger;
    static Vector3 bulletdir;
    static Vector3 gunpos;
    private static byte[] bbuffer = new byte[512];

    //score
    int oldscore;
    //map
    static int currentMap = 1;
    public GameObject de1;
    public GameObject de2;
    void Start()
    {
        //reset
        currentMap = 1;
        connected = false;
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        //
        temp = GameObject.FindGameObjectWithTag("IP");
        //if (temp == null)
        //    SceneManager.LoadScene(0);
        string getip = temp.GetComponent<IPenter>().getString();
        //client.Connect(IPAddress.Parse(getip), 8888);
        client.BeginConnect(IPAddress.Parse(getip), 8888, new AsyncCallback(ConnectCallBack), null);

        trigger = false;
        oldscore = teamscore.instance.getScore4team2();

        //initial p2 possition
        pos = enemy.transform.position;
        facing = enemy.transform.rotation.eulerAngles;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if(currentMap==1)
        {
            de1.SetActive(true);
            de2.SetActive(false);
        }
        if (currentMap == 2)
        {
            de1.SetActive(false);
            de2.SetActive(true);
        }
        //pos = new float[] { player.transform.position.x, player.transform.position.y, player.transform.position.z };
        //bpos = new byte[pos.Length * 4];
        //Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);
        //client.Send(bpos);
        if (connected)
        {

                //send player data
                sposA = new float[] { player.transform.position.x, player.transform.position.y, player.transform.position.z };
                sfacingA = new float[] { player.transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z };
                sendbuffer = new byte[25];
                Buffer.BlockCopy(sposA, 0, sendbuffer, 0, 12);
                Buffer.BlockCopy(sfacingA, 0, sendbuffer, 12, 12);
                client.Send(sendbuffer);




            //send local score
            int newscore = teamscore.instance.getScore4team2();
            if (newscore != oldscore)
            {
                int localscore = teamscore.instance.getScore4team2();
                byte[] scoreA = BitConverter.GetBytes(localscore);
                client.Send(scoreA);
                oldscore = newscore;
            }
            //recv part
            client.BeginReceive(recvbuffer, 0, recvbuffer.Length, 0, new AsyncCallback(ReceiveCallback), client);
            enemy.transform.position = pos;
            enemy.transform.rotation = Quaternion.Euler(facing);
            //bullet
            if (trigger == true)
            {
                enemyshoot();
            }
        }
        else
            timer += Time.deltaTime;
        if (timer >= timeout)
        {
            Destroy(temp);
            PauseMenu.ingaming = false;
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene(1); 
        }
    }
    private static void ReceiveCallback(IAsyncResult result)
    {
        Socket socket = (Socket)result.AsyncState;
        int rec = socket.EndReceive(result); //12 for a vector3
        //Debug.Log("recved int: " + rec);
        
        if (rec == 25)
        {
            posA = new float[3];        //player recv
            Buffer.BlockCopy(recvbuffer, 0, posA, 0, 12);
            pos = new Vector3(posA[0], posA[1], posA[2]);
            facingA = new float[3];        //facing
            Buffer.BlockCopy(recvbuffer, 12, facingA, 0, 12);
            facing = new Vector3(facingA[0], facingA[1], facingA[2]);
        }
        if (rec == 24)
        {
            Debug.Log("get shot");
            float[] temp = new float[3];
            float[] temp2 = new float[3];
            Buffer.BlockCopy(recvbuffer, 0, temp, 0, 12);
            Buffer.BlockCopy(recvbuffer, 12, temp2, 0, 12);
            bulletdir = new Vector3(temp[0], temp[1], temp[2]);
            gunpos = new Vector3(temp2[0], temp2[1], temp2[2]);
            trigger = true;
        }
        if(rec ==4)
        {
            int score = BitConverter.ToInt32(recvbuffer);
            teamscore.instance.setscore4team1(score);
        }
        client.BeginReceive(recvbuffer, 0, recvbuffer.Length, 0, new AsyncCallback(ReceiveCallback), socket);
    }

    void enemyshoot()
    {
        //GameObject newbullet = GameObject.Instantiate(cube1, gunpos, Quaternion.identity);
        GameObject newbullet = objectPooler.instance.getFromPool("bullet", gunpos, Quaternion.identity);
        newbullet.GetComponent<snowball>().setshooter(enemy, bulletdir);
        Rigidbody rb = newbullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(bulletdir * 25, ForceMode.Impulse);
        trigger = false;
    }

    private static void SendCallback(IAsyncResult result)
    {
        Socket socket = (Socket)result.AsyncState;
        socket.EndSend(result);
    }
    public static void sendbullet2(Vector3 dir, Vector3 gun)
    {
        bbuffer = new byte[24];
        float[] temp = { dir.x, dir.y, dir.z };
        float[] temp2 = { gun.x, gun.y, gun.z };
        Buffer.BlockCopy(temp, 0, bbuffer, 0, 12);
        Buffer.BlockCopy(temp2, 0, bbuffer, 12, 12);
       
        client.BeginSend(bbuffer, 0, bbuffer.Length, 0, new AsyncCallback(SendCallback), client);
       
    }

    private static void ConnectCallBack(IAsyncResult result)
    {
        client.BeginReceive(recvbuffer, 0, recvbuffer.Length, 0, new AsyncCallback(CheckCallBack), client);
    }
    private static void CheckCallBack(IAsyncResult result)
    {
    Socket socket = (Socket)result.AsyncState;
    int rec = socket.EndReceive(result); //12 for a vector3
        //Debug.Log("recved int: " + rec);
        if(rec ==3)
        {
            currentMap = 1;
            connected = true;           
        }
        if (rec == 2)
        {
            currentMap = 2;
            connected = true;
        }
    }
    public static void clientclose()
    {
        client.Close();
        Debug.Log("client close");
    }
}
