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

public class ChatManager : MonoBehaviour
{
    public Slider textSizeSlider;
    private WebSocketSharp.WebSocket m_Socket = null;
    public static ChatManager instance = null;
    public Transform chatField;
    public GameObject chatText;
    public InputField mainInputField;
    private List<string> newChat;
    private Data data;
    public ChatController chatController;
    void Awake()
    {
        chatController = new ChatController();
        chatController.SetMaxLogSize(3);
        newChat = new List<string>();
        textSizeSlider.value = 20;
    }

    public void Start()
    {
        if (instance == null)
            instance = this;
        m_Socket = new WebSocketSharp.WebSocket("ws://localhost:3000");
        m_Socket.OnMessage += Recv;
        m_Socket.Connect();

        mainInputField.onEndEdit.AddListener(var => { 
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                LockInput(mainInputField); 
        });
        textSizeSlider.onValueChanged.AddListener(delegate
        {
            chatController.ChangeTextSize((int)textSizeSlider.value);
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
        tempObject.SendMessage("ChangeSize", (int)textSizeSlider.value);
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

