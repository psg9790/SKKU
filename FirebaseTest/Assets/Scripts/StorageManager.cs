using Firebase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using Firebase.Extensions;
using TMPro;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;

public class StorageManager : MonoBehaviour
{
    public TMP_InputField localPath;
    public TMP_InputField storagePath;

    FirebaseStorage storage;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(t =>
        {
            if (t.Result == Firebase.DependencyStatus.Available)
            {

            }
            else
            {
                Debug.LogError("init error");
            }
        });
        storage = FirebaseStorage.DefaultInstance;
    }

    public void UploadFile()
    {
        string localFilePath = localPath.text;
        string storageFilePath = storagePath.text;

        StorageReference storageRef = storage.GetReference(storageFilePath);

        storageRef.PutFileAsync(localFilePath).ContinueWithOnMainThread(t =>
        {
            if(t.IsFaulted || t.IsCanceled)
            {
                Debug.LogError("upload error");
                return;
            }
            Debug.Log("upload success");
        });
    }

    public void UploadFiles() // 파이어베이스 업로드
    {
        var customBytes = System.IO.File.ReadAllBytes("c:/tmp/low_city.png");

        // Create a reference to the file you want to upload
        StorageReference riversRef = FirebaseStorage.DefaultInstance.GetReference("").Child("c:/tmp/low_city.png");

        // Upload the file to the path
        riversRef.PutBytesAsync(customBytes).ContinueWith((Task<StorageMetadata> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
            }
            else
            {
                StorageMetadata metadata = task.Result;
                string md5hash = metadata.Md5Hash;
                Debug.Log("finished file uploading...");
                Debug.Log($"md5hash: {md5hash}");
            }
        });
    }
}
