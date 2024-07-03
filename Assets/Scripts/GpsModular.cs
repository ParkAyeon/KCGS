using System;
using System.Collections;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

// using Debug = System.Diagnostics.Debug;

public class GpsModular : MonoBehaviour
{
    public SocketIOUnity socket;

    public float f_Lat;
    public float f_Long;

    private string strLat;
    private string strLong;

    private float fDestroyTime = 2f;
    private float fTickTime = 0;
    // Start is called before the first frame update

    public void GpsConnect(string d) // �����κ��� �޾ƿ��� �����ʹ� d , ���� d �����Ϳ��� ���� �浵 �����Ͱ� ���ļ� �޾��� 37.494/126.959 ���·�
    {
        strLong = d.Substring(d.IndexOf("/") + 1).Trim();  // �޾��� �����͸� "/" ���ڸ� �������� strLong�� strLat �� ������ ���Ҵ�
        strLat = d.Substring(0, d.IndexOf("/")); 

        f_Lat = float.Parse(strLat);
        f_Long = float.Parse(strLong);

        Debug.Log("Received Data: " + strLat + " " + strLong);
    }

    void Start()
    {
        var uri = new Uri("http://gsclab.synology.me:8080/");
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
            ,
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        socket.JsonSerializer = new NewtonsoftJsonSerializer();

        ///// reserved socketio events
        // socket.OnConnected += (sender, e) =>
        // {
        //     Debug.Print("socket.OnConnected");
        // };
        // socket.OnPing += (sender, e) =>
        // {
        //     Debug.Print("Ping");
        // };
        // socket.OnPong += (sender, e) =>
        // {
        //     Debug.Print("Pong: " + e.TotalMilliseconds);
        // };
        // socket.OnDisconnected += (sender, e) =>
        // {
        //     Debug.Print("disconnect: " + e);
        // };
        // socket.OnReconnectAttempt += (sender, e) =>
        // {
        //     Debug.Print($"{DateTime.Now} Reconnecting: attempt = {e}");
        // };
        // ////
        // ///
        // Debug.Print("Connecting...");
        socket.Connect();
        socket.On("testEvent", (response) =>
        {
            string text = response.GetValue<string>();
            GpsConnect(text);
        });    
    }

    // Update is called once per frame
    void Update()
    {
        fTickTime += Time.deltaTime;
        if (fTickTime >= fDestroyTime) // update ���� �߿� ���� �ð� ������ ����ϱ� ���� �ð� ����
        {
            socket.On("testEvent", (response) =>
            {
                string text = response.GetValue<string>();
                GpsConnect(text);
            });    
        }
        fTickTime = 0;
    }
}
