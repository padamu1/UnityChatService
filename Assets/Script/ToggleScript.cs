using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    public Toggle toggleOptOne;
    public Toggle toggleOptTwo;
    public Toggle toggleOptTrd;

    void Start()
    {
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

    void ToggleOptOneChanged(Toggle change)
    {
        if(change.isOn)
        {
            ChatManager.instance.chatController.SetMaxLogSize(1);
        }
    }
    void ToggleOptTwoChanged(Toggle change)
    {
        if (change.isOn)
        {
            ChatManager.instance.chatController.SetMaxLogSize(2);
        }
    }
    void ToggleOptTrdChanged(Toggle change)
    {
        if (change.isOn)
        {
            ChatManager.instance.chatController.SetMaxLogSize(3);
        }
    }
}
