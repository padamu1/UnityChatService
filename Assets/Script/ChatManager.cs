using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;

[System.Serializable]
public class Data
{
    public string type;
    public string data;
}

public class ChatController
{
    private List<GameObject> chatLog;
    private int maxLogSize;
    public ChatController()
    {
        chatLog = new List<GameObject>();
    }
    //Please initialize this value before execute SetChatLog function
    public void SetMaxLogSize(int maxLogSize)
    {
        this.maxLogSize = maxLogSize;

        // execute below code if maxLogSize is different from before
        if (maxLogSize < chatLog.Count)
        {
            for(int i = 0; i < chatLog.Count - maxLogSize; i++)
            {
                GameObject.Destroy(chatLog[0]);
                chatLog.RemoveAt(0);
            }
        }
    }

    public void SetChatLog(GameObject chat)
    {

        chatLog.Add(chat);
        if(maxLogSize < chatLog.Count)
        {
            GameObject.Destroy(chatLog[0]);
            chatLog.RemoveAt(0);
        }

    }
}

public class ChatManager : MonoBehaviour
{
    public static WebSocketSharp.WebSocket m_Socket = null;

    public Transform chatField;
    public GameObject chatText;
    public InputField mainInputField;
    private List<string> newChat;
    private Data data;
    private ChatController chatController;
    void Awake()
    {
        chatController = new ChatController();
        chatController.SetMaxLogSize(3);
        newChat = new List<string>();
    }

    public void Start()
    {
        m_Socket = new WebSocketSharp.WebSocket("ws://localhost:3000");
        m_Socket.OnMessage += Recv;
        m_Socket.Connect();

        mainInputField.onEndEdit.AddListener(var => { 
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                LockInput(mainInputField); 
        });
    }

    void Update()
    {
        if(newChat.Count > 0)
        {
            MakeChat(newChat[0]);
        }
    }
    //Message handling - Create GameObject and Add chatLog
    public void Recv(object sender, MessageEventArgs e)
    {
        newChat.Add(e.Data);
    }

    void MakeChat(string text)
    {

        GameObject tempObject = Instantiate(chatText, chatField);
        tempObject.SendMessage("SetText", text);
        chatController.SetChatLog(tempObject);
        newChat.RemoveAt(0);
    }
    //Message Upload
    void LockInput(InputField input)
    {
        data = new Data();
        if (input.text.Length > 0)
        {
            data.type = "Ã¤ÆÃ";
            data.data = input.text;
            m_Socket.Send(JsonUtility.ToJson(data));
            input.text = "";
        }
        Debug.Log("Enter");
    }
}

