using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Extensions;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StorageUploader : MonoBehaviour
{
    FirebaseStorage storage;
    public TMP_InputField localFilePath;
    public TMP_InputField storagePath;
    public RawImage displayImage;

    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
    }

    public void UploadFile()
    {
        string LocalFilePath = localFilePath.text;
        string StoragePath = storagePath.text;
        //"C:/Users/user/Downloads/3211-bump.jpg"
        //"C:/Users/user/Downloads/Team_Note.pdf"
        StorageReference storageReference = storage.GetReference(StoragePath);
        storageReference.PutFileAsync(LocalFilePath).ContinueWithOnMainThread(t =>
        {
            if (t.IsFaulted)
            {
                Debug.LogError($"upload fault\n" +
                    $"{t.Exception.ToString()}");
                return;
            }
            if (t.IsCanceled)
            {
                Debug.LogError("upload cancel");
                return;
            }
            if (t.IsCompleted)
            {
                Debug.Log("upload success");
                GetDownloadUrl(storageReference);
            }
        });
    }

    private void GetDownloadUrl(StorageReference storageRef)
    {
        storageRef.GetDownloadUrlAsync().ContinueWithOnMainThread(t =>
        {
            if (t.IsFaulted || t.IsCanceled)
            {
                Debug.LogError("");
                return;
            }
            string downloadURL = t.Result.ToString();
            Debug.Log($"download url: {downloadURL}");
            StartCoroutine(DownloadImage(downloadURL));
        });
    }

    IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error download image : " + request.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            displayImage.texture = texture;
        }
    }
}
