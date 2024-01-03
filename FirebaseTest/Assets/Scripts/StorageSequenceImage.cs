using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSequenceImage : MonoBehaviour
{
    FirebaseStorage storage;
    List<string> localImagePath;
    string folderPathInStorage;

    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        localImagePath = new List<string>();
        folderPathInStorage = "Sequence/";

        for (int i = 1; i <= 10; i++)
        {
            localImagePath.Add("c:/tmp/Sequence/final_output_" + string.Format("{0:D2}", i) + ".png");
        }
        StartCoroutine(UploadSequenceImage());
    }

    IEnumerator UploadSequenceImage()
    {
        foreach(var imagePath in localImagePath)
        {
            yield return UploadImage(imagePath);
        }
        Debug.Log("sequence upload complete");
    }

    IEnumerator UploadImage(string localPath)
    {
        string fileName = System.IO.Path.GetFileName(localPath);
        string storagePath = folderPathInStorage + fileName;

        StorageReference storageRef = storage.GetReference(storagePath);
        var task = storageRef.PutFileAsync(localPath);

        yield return new WaitUntil(() => task.IsCompleted);
        if(task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("upload Failed");
            yield break;
        }
        Debug.Log($"{localPath} uploaded");
    }
}
