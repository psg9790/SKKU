using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirestoreController : MonoBehaviour
{
    FirebaseFirestore db;

    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    public void AddData()
    {
        DocumentReference docRef = db.Collection("users").Document("user_id");
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "first", "aaa" }, { "last", "sss" }, { "born", "ddd" }
        };
        docRef.SetAsync(user).ContinueWithOnMainThread(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Log("addData failed");
                return;
            }
            if (t.IsCanceled)
            {
                Debug.Log("addData canceled");
                return;
            }
            Debug.Log("addData complete");
        });
    }

    public void ReadData()
    {
        DocumentReference docRef = db.Collection("users").Document("user_id");
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Log("readData failed");
                return;
            }
            if (t.IsCanceled)
            {
                Debug.Log("readData canceled");
                return;
            }
            DocumentSnapshot snapshot = t.Result;
            if (snapshot.Exists)
            {
                Dictionary<string, object> user = snapshot.ToDictionary();
                string firstname = user["first"].ToString();
                Debug.Log(firstname);
            }
        });
    }

    public void ReadAllData()
    {
        FirebaseFirestore allDB = FirebaseFirestore.DefaultInstance;

        allDB.Collection("Data").GetSnapshotAsync().ContinueWithOnMainThread(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Log("readAllData failed");
                return;
            }
            if (t.IsCanceled)
            {
                Debug.Log("readAllData canceled");
                return;
            }
            QuerySnapshot snapshot = t.Result;
            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                Debug.Log($"Document {doc.Id}");
                Dictionary<string, object> documentData = doc.ToDictionary();
                foreach(var keyValPair in documentData)
                {
                    Debug.Log($"{keyValPair.Key}:{keyValPair.Value}");
                }
            }
        });
    }
}
