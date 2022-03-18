using UnityEngine;
using System.Collections;
using WebSocketSharp;
using System;
public class SocketManager : MonoBehaviour
{
    public WebSocketSharp.WebSocket m_Socket = null;
    private void Start()
    {
        m_Socket = new WebSocketSharp.WebSocket("ws://localhost:3000");
        m_Socket.OnMessage += Recv;
        m_Socket.Connect();
    }

    public void Connect()
    {

    }

    public void Recv(object sender, MessageEventArgs e)
    {
        Debug.Log(e.Data);

    }

}