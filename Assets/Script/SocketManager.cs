using UnityEngine;
using System.Collections;
using WebSocketSharp;
using System;

[System.Serializable]
public class PositionData
{
    public string type;
    public float x;
    public float y;
    public float z;
}

public class SocketManager : MonoBehaviour
{
    public static WebSocketSharp.WebSocket m_Socket = null;
    PositionData positionData;
    private void Start()
    {
        positionData = new PositionData();
        positionData.type = "위치정보";
        m_Socket = new WebSocketSharp.WebSocket("ws://localhost:3000");
        m_Socket.OnMessage += Recv;
        m_Socket.Connect();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            positionData.x = 0.1f;
            positionData.y = 0.1f;
            positionData.z = 0.1f;
            m_Socket.Send(JsonUtility.ToJson(positionData));
        }
    }

    public void Recv(object sender, MessageEventArgs e)
    {
        Debug.Log(e.Data);
    }
}