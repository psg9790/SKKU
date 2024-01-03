using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using Firebase.Extensions;
using System;
using UnityEngine.SceneManagement;
using Firebase.Database;
using UnityEngine.UI;

public class FirebaseController : MonoBehaviour
{
    private FirebaseAuth auth;
    private FirebaseUser user;

    public TMP_InputField usernameInput;
    public TMP_InputField messageInput;

    public MessageBox messageBoxPrefab;
    public Transform messageContentTrans;

    public ScrollRect scrollRect;

    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if(task.Result == Firebase.DependencyStatus.Available)
            {
                FIrebaseInit();
            }
            else
            {
                Debug.LogError("");
            }
        });
    }

    void FIrebaseInit()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        // RealtimeDB의 값이 바뀌는 이벤트가 발생하면, 마지막 하나의 값을 읽어오는 함수를 실행함
        FirebaseDatabase.DefaultInstance.GetReference("ChatMessage").LimitToLast(1).ValueChanged += ReceiveMessage;
    }

    void AuthStateChanged(object sender, EventArgs e)
    {
        FirebaseAuth senderAuth = sender as FirebaseAuth;
        if(senderAuth != null)
        {
            user = senderAuth.CurrentUser;
            if(user != null)
            {
                Debug.Log(user.UserId);
            }
        }
    }

    public void SignIn()
    {
        SignInAnonymous();
    }
    Task SignInAnonymous()
    {
        return auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsFaulted)
            {
                Debug.Log("signIn fail");
                return;
            }
            if(task.IsCanceled)
            {
                Debug.Log("signIn cancele");
                return;
            }
            Debug.Log("signIn complete");
            SceneManager.LoadScene(1);
        });
    }

    public void ReadChatMessage()
    {
        DatabaseReference chatDB = FirebaseDatabase.DefaultInstance.GetReference("ChatMessage");
        chatDB.GetValueAsync().ContinueWithOnMainThread(t =>
        {
            if(t.IsFaulted)
            {
                Debug.Log("read error");
                return;
            }
            if(t.IsCanceled)
            {
                Debug.Log("read canceled");
                return;
            }
            DataSnapshot dataSnapshot = t.Result;
            Debug.Log(dataSnapshot.ChildrenCount);
            foreach(var child in dataSnapshot.Children)
            {
                Debug.Log(child.Key + " " + child.Child("username").Value + " " + child.Child("message").Value);
            }
        });
    }

    /// <summary>
    /// 메세지 보내기, 버튼 OnClick() 이벤트에 등록
    /// </summary>
    public void SendChatMessage()
    {
        DatabaseReference chatdb = FirebaseDatabase.DefaultInstance.GetReference("ChatMessage");
        string key = chatdb.Push().Key; // Firebase에서 랜덤으로 키값을 생성해줌

        // <key, value> 요소들을 딕셔너리에 삽입
        Dictionary<string, string> msgDic = new Dictionary<string, string>();
        msgDic.Add("username", usernameInput.text);
        msgDic.Add("message", messageInput.text);

        // 위에서 만든 딕셔너리를 object 타입으로 boxing해서
        // 새로운 업로드용 딕셔너리 생성
        Dictionary<string, object> uploadDic = new Dictionary<string, object>();
        uploadDic.Add(key, msgDic);

        chatdb.UpdateChildrenAsync(uploadDic).ContinueWithOnMainThread(t =>
        {
            if (t.IsCompleted)
                Debug.Log("upload complete");
        });
    }

    /// <summary>
    /// 타깃 DatabaseReference(여기서는 "ChatMessage")의 값 변경시 일어나는 event에 listener로 등록할 메서드
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">이쪽으로 변경된 snapshot이 넘어옴</param>
    public void ReceiveMessage(object sender, ValueChangedEventArgs e)
    {
        DataSnapshot snapshot = e.Snapshot;
        Debug.Log(snapshot.ChildrenCount);  // 채팅 개수 출력

        foreach(var message in snapshot.Children)
        {
            //Debug.Log(message.Key + "  " + message.Child("username").Value.ToString() + "  " + 
            //    message.Child("message").Value.ToString());

            string userName = message.Child("username").Value.ToString();
            string msg = message.Child("message").Value.ToString();
            AddChatBox(userName, msg);
        }
    }

    /// <summary>
    /// 메세지 UI 프리팹을 생성하고, 값을 넣어주는 함수
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="message"></param>
    public void AddChatBox(string userName, string message)
    {
        MessageBox mbox = Instantiate<MessageBox>(messageBoxPrefab, messageContentTrans);
        mbox.SetMessage(userName, message);

        StartCoroutine(AdjustScrollPosition(mbox.GetComponent<RectTransform>()));
    }

    /// <summary>
    /// ScrollView에 값 추가 시, 화면을 가장 아래로 내려주는 기능
    /// </summary>
    /// <param name="newMessageBox"></param>
    /// <returns></returns>
    IEnumerator AdjustScrollPosition(RectTransform newMessageBox)
    {
        yield return new WaitForEndOfFrame();

        float newY = -newMessageBox.anchoredPosition.y - (newMessageBox.sizeDelta.y * newMessageBox.pivot.y);

        float contentHeight = messageContentTrans.GetComponent<RectTransform>().sizeDelta.y;

        float scrollViewHeight = scrollRect.GetComponent<RectTransform>().sizeDelta.y;

        newY = newY - (scrollViewHeight * 0.5f);

        messageContentTrans.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, newY);
    }
}
