using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI systemMessageText;
    public string CurrentMessage { get; private set; }
    public string zKeyMessage;
    public string xKeyMessage;
    public string cKeyMessage;

    public void Start()
    {
        UpdateMessage();
    }
    public void ChangeZKeyMessage(string message)
    {
        zKeyMessage = message;
        UpdateMessage();
    }
    public void ChangeXKeyMessage(string message)
    {
        xKeyMessage = message;
        UpdateMessage();
    }
    public void ChangeCKeyMessage(string message)
    {
        cKeyMessage = message;
        UpdateMessage();
    }
    public void ShowMessage(string message)
    {
        CurrentMessage = message;
        systemMessageText.text = message;
        systemMessageText.gameObject.SetActive(true);
    }

    public void UpdateMessage()
    {
        CurrentMessage = zKeyMessage + "  " + xKeyMessage + "  " + cKeyMessage;
        systemMessageText.text = CurrentMessage;
        if(string.IsNullOrWhiteSpace(CurrentMessage))
        {
            systemMessageText.gameObject.SetActive(false);
            Debug.Log("SystemMessageUI: No message to display.");
        }
        else
        {
            systemMessageText.gameObject.SetActive(true);
        }
    }
}
