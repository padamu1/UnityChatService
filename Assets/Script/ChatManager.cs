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
    public GameObject scrollbarVerticle;
    public Slider textSizeSlider;
    public float initTextSliderValue;
    public float minValue;
    public float maxValue;
    public Transform chatField;
    public GameObject chatText;
    public InputField mainInputField;

    public Toggle toggleOptOne;
    public Text toggleOneLabel;
    public Toggle toggleOptTwo;
    public Text toggleTwoLabel;
    public Toggle toggleOptTrd;
    public Text toggleTrdLabel;

    public int toggleOneValue;
    public int toggleTwoValue;
    public int toggleTrdValue;

    private List<string> newChat;
    private Data data;
    private ChatController chatController;
    private WebSocketSharp.WebSocket m_Socket = null;
    private bool chatAdd;

    void Awake()
    {
        chatAdd = false;
        chatController = new ChatController();
        chatController.SetMaxLogSize(toggleTrdValue);
        newChat = new List<string>();
        textSizeSlider.value = initTextSliderValue;
        toggleOneLabel.text = "" + toggleOneValue;
        toggleTwoLabel.text = "" + toggleTwoValue;
        toggleTrdLabel.text = "" + toggleTrdValue;
        textSizeSlider.minValue = minValue;
        textSizeSlider.maxValue = maxValue;
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
        textSizeSlider.onValueChanged.AddListener(delegate
        {
            chatController.ChangeTextSize((int)textSizeSlider.value);
        });

        toggleOptOne.onValueChanged.AddListener(delegate
        {
            ToggleOptOneChanged(toggleOptOne);
        });
        toggleOptTwo.onValueChanged.AddListener(delegate
        {
            ToggleOptTwoChanged(toggleOptTwo);
        });
        toggleOptTrd.onValueChanged.AddListener(delegate
        {
            ToggleOptTrdChanged(toggleOptTrd);
        });
    }

    void Update()
    {
        if (newChat.Count > 0)
        {
            MakeChat(newChat[0]);
        }
    }
    void FixedUpdate()
    {
        if (chatAdd & scrollbarVerticle.activeSelf)
        {
            scrollbarVerticle.GetComponent<Scrollbar>().value = 0f;
            chatAdd = false;
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
        chatAdd = true;
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


    void ToggleOptOneChanged(Toggle change)
    {
        if (change.isOn)
        {
            chatController.SetMaxLogSize(toggleOneValue);
        }
    }
    void ToggleOptTwoChanged(Toggle change)
    {
        if (change.isOn)
        {
            chatController.SetMaxLogSize(toggleTwoValue);
        }
    }
    void ToggleOptTrdChanged(Toggle change)
    {
        if (change.isOn)
        {
            chatController.SetMaxLogSize(toggleTrdValue);
        }
    }
}

