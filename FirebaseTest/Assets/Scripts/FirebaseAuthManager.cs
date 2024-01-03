using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth;
    public TMP_InputField email;
    public TMP_InputField password;

    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;    
    }

    public void Create()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => 
        { 
            if(task.IsFaulted)
            {
                Debug.Log(task.Exception.ToString());
                Debug.Log("���� ���� ����");
                return;
            }
            if(task.IsCanceled)
            {
                Debug.Log("���� ���� ���");
                return;
            }
            FirebaseUser newUser = task.Result.User;
            Debug.Log("���� ���� ����");
        });
    }
    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("�α��� ����");
                return;
            }
            if (task.IsCanceled)
            {
                Debug.Log("�α��� ���");
                return;
            }
            FirebaseUser newUser = task.Result.User;
            Debug.Log("�α��� ����");
        });
    }
    public void Logout()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
    }
}
