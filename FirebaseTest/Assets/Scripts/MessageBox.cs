using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageBox : MonoBehaviour
{
    public TextMeshProUGUI userNameLabel;
    public TextMeshProUGUI messageLabel;

    public void SetMessage(string username, string message)
    {
        userNameLabel.text = username;
        messageLabel.text = message;
    }
}
