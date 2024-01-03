using Firebase.Firestore;
using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Extensions;

public class FirestoreUploadData : MonoBehaviour
{
    FirebaseStorage storage;
    FirebaseFirestore db;
    public TMP_InputField localFilePath;
    public TMP_InputField storagePath;

    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;
    }

    public void UploadFile()
    {
        string LocalFilePath = localFilePath.text;
        string StoragePath = storagePath.text;

        StorageReference storageRef = storage.GetReference(StoragePath);
        storageRef.PutFileAsync(LocalFilePath).ContinueWithOnMainThread(t =>
        {
            if(t.IsCompleted)
            {
                Debug.Log("file upload successfully");
                GetDownloadURL(storageRef);
            }
            else
            {
                Debug.Log("file upload failed");
            }
        });
    }

    private void GetDownloadURL(StorageReference storageRef)
    {
        storageRef.GetDownloadUrlAsync().ContinueWithOnMainThread(t =>
        {
            if(t.IsCompleted)
            {
                Debug.Log("file upload successfully");
                string downloadURL = t.Result.ToString();
                SaveToFirestore(downloadURL);
            }
            else
            {
                Debug.Log("getting url failed");
            }
        });
    }

    private void SaveToFirestore(string downloadURL)
    {
        var imageData = new Dictionary<string, object>
        {
            { "url", downloadURL },
            { "timestamp", FieldValue.ServerTimestamp }
        };
        db.Collection("UpImage").AddAsync(imageData).ContinueWithOnMainThread(t =>
        {
            if(t.IsCompleted)
            {
                Debug.Log($"success\n");
            }
            else
            {
                Debug.Log("fail");
            }
        });
    }
}
