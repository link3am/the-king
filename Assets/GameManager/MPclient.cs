using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;


public class MPclient : MonoBehaviour
{

    private static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    static EndPoint serverEP = new IPEndPoint(IPAddress.Parse("10.0.0.126"), 8888);
    public GameObject player;

    private static byte[] sendbuffer = new byte[512];
    private static float[] sposA;
    private static float[] sfacingA;


    //recv
    public GameObject enemy;
    private static byte[] recvbuffer = new byte[512];
    private static float[] posA;
    private static Vector3 pos;
    private static float[] facingA;
    private static Vector3 facing;
    //bullet
    public GameObject cube1;
    static bool trigger;

    static Vector3 bulletdir;
    static Vector3 gunpos;
    void Start()
    {

        client.Connect(IPAddress.Parse("10.0.0.126"), 8888);
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        //pos = new float[] { player.transform.position.x, player.transform.position.y, player.transform.position.z };
        //bpos = new byte[pos.Length * 4];
        //Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);
        //client.Send(bpos);

        //send player data
        sposA = new float[] { player.transform.position.x, player.transform.position.y, player.transform.position.z };
        sfacingA = new float[] { player.transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z };
        sendbuffer = new byte[25];
        Buffer.BlockCopy(sposA, 0, sendbuffer, 0, 12);
        Buffer.BlockCopy(sfacingA, 0, sendbuffer, 12, 12);
        client.Send(sendbuffer);

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
            Debug.Log("bullet recved ");
            float[] temp = new float[3];
            float[] temp2 = new float[3];
            Buffer.BlockCopy(recvbuffer, 0, temp, 0, 12);
            Buffer.BlockCopy(recvbuffer, 12, temp2, 0, 12);
            bulletdir = new Vector3(temp[0], temp[1], temp[2]);
            gunpos = new Vector3(temp2[0], temp2[1], temp2[2]);

            Debug.Log(bulletdir + "  " + gunpos);
            trigger = true;
        }

        client.BeginReceive(recvbuffer, 0, recvbuffer.Length, 0, new AsyncCallback(ReceiveCallback), socket);
    }

    void enemyshoot()
    {
        Debug.Log("running");
        GameObject newbullet = GameObject.Instantiate(cube1, gunpos, Quaternion.identity);
        newbullet.GetComponent<snowball>().setshooter(enemy, bulletdir);
        Rigidbody rb = newbullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(bulletdir * 25, ForceMode.Impulse);
        trigger = false;
    }
}
