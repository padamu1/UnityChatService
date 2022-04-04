using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log(maxLogSize);
        this.maxLogSize = maxLogSize;

        // execute below code if maxLogSize is different from before
        if (maxLogSize < chatLog.Count)
        {
            for (int i = 0; i <= chatLog.Count - maxLogSize; i++)
            {
                GameObject.Destroy(chatLog[0]);
                chatLog.RemoveAt(0);
            }
        }
    }

    public void SetChatLog(GameObject chat)
    {

        chatLog.Add(chat);
        if (maxLogSize < chatLog.Count)
        {
            GameObject.Destroy(chatLog[0]);
            chatLog.RemoveAt(0);
        }

    }

    public void ChangeTextSize(int textSize)
    {
        foreach (GameObject sample in chatLog)
        {
            sample.SendMessage("ChangeSize", textSize);
        }
    }

}