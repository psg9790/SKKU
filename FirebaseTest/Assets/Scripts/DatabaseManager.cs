using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using TMPro;
using Firebase.Extensions;

public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField levelInput;
    public TMP_InputField goldInput;
    public TMP_Text action;

    public string name;
    public string level;
    public string gold;

    public class Data
    {
        public string level;
        public string gold;

        public Data(string level, string gold)
        {
            this.level = level;
            this.gold = gold;
        }
    }
    private DatabaseReference databaseReference;

    // Start is called before the first frame update
    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnClickSaveButton()
    {
        name = nameInput.text;
        level = levelInput.text;
        gold = goldInput.text;

        var data = new Data(level, gold);
        string jsonData = JsonUtility.ToJson(data);

        databaseReference.Child(name).SetRawJsonValueAsync(jsonData);

        action.text = "saved";
    }

    public void OnClickLoadButton()
    {
        databaseReference.Child(nameInput.text).GetValueAsync().ContinueWithOnMainThread(t =>
        {
            if(t.IsFaulted)
            {
                action.text = "faulted";
                return;
            }
            if(t.IsCanceled)
            {
                action.text = "canceled";
                return;
            }
            action.text = "load success";
            DataSnapshot dataSnapshot = t.Result;
            string dataString = "";
            foreach(var item in dataSnapshot.Children)
            {
                dataString += item.Key + " " + item.Value + '\n';
            }
            action.text = dataString;
        });
    }
}
