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




public class MPhoster : MonoBehaviour
{

    private static Socket server;
    private static Socket client1;
    private static byte[] recvbuffer = new byte[512];
    private static float[] posA;
    private static Vector3 pos;
    private static float[] facingA;
    private static Vector3 facing;
    public GameObject enemy;
    //bullet
    public static Vector3 bulletforce;
    private static byte[] bbuffer = new byte[512];

    //send local player
    public GameObject splayer;
    private static float[] sposA;
    private static float[] sfacingA;
    private static byte[] sendbuffer = new byte[512]; 
    void Start()
    {
        server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
       // string ipadress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();//reference https://stackoverflow.com/questions/8709427/get-local-system-ip-address-using-c-sharp
        IPAddress ip = IPAddress.Parse("10.0.0.126");
        server.Bind(new IPEndPoint(ip, 8888));//a
        server.Listen(10);
        Debug.Log("start server");

        pos = Vector3.zero;
        facing = Vector3.zero;
        server.BeginAccept(new AsyncCallback(AcceptCallback), null);

    }

    // Update is called once per frame
    void Update()
    {

        enemy.transform.position = pos;
        enemy.transform.rotation = Quaternion.Euler(facing);       

        //send player data
        sposA = new float[] { splayer.transform.position.x, splayer.transform.position.y, splayer.transform.position.z };
        sfacingA = new float[] { splayer.transform.eulerAngles.x, splayer.transform.eulerAngles.y, splayer.transform.eulerAngles.z };
        sendbuffer = new byte[25];
        Buffer.BlockCopy(sposA, 0, sendbuffer, 0, 12);
        Buffer.BlockCopy(sfacingA, 0, sendbuffer, 12, 12);



    }

    private static void AcceptCallback(IAsyncResult result)
    {
        Socket client = server.EndAccept(result);
        IPEndPoint clientEP = (IPEndPoint)client.RemoteEndPoint;
        string ip = clientEP.Address.ToString();
        Debug.Log("client connected!!!   IP: " + ip);

   
        client.BeginReceive(recvbuffer, 0, recvbuffer.Length, 0, new AsyncCallback(ReceiveCallback), client);
    }

    private static void ReceiveCallback(IAsyncResult result)
    {
        client1 = (Socket)result.AsyncState;
        int rec = client1.EndReceive(result); //12 for a vector3
        if (rec == 25)//its player 
        {
            posA = new float[3];            //position
            Buffer.BlockCopy(recvbuffer, 0, posA, 0, 12);
            pos = new Vector3(posA[0], posA[1], posA[2]);
            facingA = new float[3];           //facing
            Buffer.BlockCopy(recvbuffer, 12, facingA, 0, 12);
            facing = new Vector3(facingA[0], facingA[1], facingA[2]);
        }
        else//its bullet
        {
            //int bulletnum = rec / 12;
            //Debug.Log("bullet: " + bulletnum);
            //temp1 = new float[3];
            //Buffer.BlockCopy(recvbuffer, 0, temp1, 0, 12);
        }

        //send 
        client1.BeginSend(sendbuffer, 0, sendbuffer.Length, 0,new AsyncCallback(SendCallback), client1);
        client1.BeginReceive(recvbuffer, 0, recvbuffer.Length, 0, new AsyncCallback(ReceiveCallback), client1);
    }
    private static void SendCallback(IAsyncResult result)
    {
        Socket socket = (Socket)result.AsyncState;
        socket.EndSend(result);
    }

    public static void sendbullet(Vector3 dir,Vector3 gun)
    {
        bbuffer = new byte[24];
        float[] temp = { dir.x, dir.y, dir.z };
        float[] temp2 = { gun.x, gun.y, gun.z };
        Buffer.BlockCopy(temp, 0, bbuffer, 0, 12);
        Buffer.BlockCopy(temp2, 0, bbuffer, 12, 12);
        if (client1 != null)
            client1.BeginSend(bbuffer, 0, bbuffer.Length, 0, new AsyncCallback(SendCallback), client1);
        else
            Debug.Log("no target");
    }
}
