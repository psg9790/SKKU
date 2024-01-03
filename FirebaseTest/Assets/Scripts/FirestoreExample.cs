using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using System.Threading.Tasks;

public class FirestoreExample : MonoBehaviour
{
    private FirebaseFirestore db;

    void Start()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;
        //AddData();
        InvokeRepeating("UpdateRandomData", 1.0f, 1.0f);
        InvokeRepeating("UpdateRandomData2", 5.0f, 5.0f);
    }

    public void AddData()
    {
        //데이터를 저장할 컬렉션과 문서 ID지정
        DocumentReference docRef = db.Collection("MyBoard").Document("MyInfo");

        //지정할 데이터 생성
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            {"data_1", 1 },
            {"data_2", 1 },
            {"data_3", 1 },
            {"data_4", 1 },
            {"data_5", 1 },
            {"Longdata_1", 10 },
            {"Longdata_2", 10 },
            {"Longdata_3", 10 },
            {"Longdata_4", 10 },
            {"Longdata_5", 10 },
        };
        //데이터 베이스에 데이터 추가
        docRef.SetAsync(user).ContinueWith(Task =>
        {
            if (Task.IsFaulted)
            {
                Debug.Log("Failed");
            }
            if (Task.IsCompleted)
            {
                Debug.Log("success");
            }
        });
    }

    public void UpdateRandomData()
    {
        DocumentReference docRef = db.Collection("MyBoard").Document("MyInfo");

        Dictionary<string, object> randomData = new Dictionary<string, object> {
            {"data_1", Random.Range(1, 11) },
            {"data_2", Random.Range(1, 11) },
            {"data_3", Random.Range(1, 11) },
            {"data_4", Random.Range(1, 11) },
            {"data_5", Random.Range(1, 11) },
        };
        docRef.UpdateAsync(randomData).ContinueWith(t =>
        {
            if(t.IsCanceled)
            {
                Debug.LogError("update random canceled");
                return;
            }
            if(t.IsFaulted)
            {
                Debug.LogError($"update random failed\n{t.Exception.ToString()}");
                return;
            }
            if(t.IsCompleted)
            {
                Debug.Log("update random success");
            }
        });
    }

    public void UpdateRandomData2()
    {
        DocumentReference docRef = db.Collection("MyBoard").Document("MyInfo");

        Dictionary<string, object> randomData = new Dictionary<string, object> {
            {"Longdata_1", Random.Range(1, 101) },
            {"Longdata_2", Random.Range(1, 101) },
            {"Longdata_3", Random.Range(1, 101) },
            {"Longdata_4", Random.Range(1, 101) },
            {"Longdata_5", Random.Range(1, 101) },
        };
        docRef.UpdateAsync(randomData).ContinueWith(t =>
        {
            if (t.IsCanceled)
            {
                Debug.LogError("update random canceled");
                return;
            }
            if (t.IsFaulted)
            {
                Debug.LogError($"update random failed\n{t.Exception.ToString()}");
                return;
            }
            if (t.IsCompleted)
            {
                Debug.Log("update random success");
            }
        });
    }
}