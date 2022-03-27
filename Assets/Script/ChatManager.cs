using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Data
{
    public string type;
    public string data;
}
public class ChatManager : MonoBehaviour
{
    private InputField mainInputField;
    Data positionData;
    void LockInput(InputField input)
    {
        Data data = new Data();
        if(input.text.Length>0)
        {
            data.type = "Ã¤ÆÃ";
            data.data = input.text;
            SocketManager.m_Socket.Send(JsonUtility.ToJson(data));
            input.text = "";
        }
        Debug.Log("Enter");
    }

    void Awake()
    {
        mainInputField = this.GetComponent<InputField>();
    }

    public void Start()
    {
        mainInputField.onEndEdit.AddListener(var => { 
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Return))
                LockInput(mainInputField); 
        });
    }

}

