using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatScript : MonoBehaviour
{
    public Text text;
    void SetText(string newText)
    {
        Debug.Log(text);
        this.text.text = newText;
    }

    void ChangeSize(int size)
    {
        text.fontSize = size;
    }
}

