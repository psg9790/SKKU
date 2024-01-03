using Firebase;
using Firebase.Firestore;
using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Firebase.Extensions;
using System.Threading.Tasks;

public class ImageUploadAndDownload : MonoBehaviour
{
    public RawImage rawImage;
    public TMP_InputField imageNameInputField;

    private FirebaseApp app;
    private FirebaseStorage storage;
    private StorageReference storageReference;
    private FirebaseFirestore firestore;
    private string uploadedUrl;

    void Start()
    {
        app = FirebaseApp.DefaultInstance;
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReference("");
        firestore = FirebaseFirestore.GetInstance(app);
    }

    public void UploadImage()
    {
        string imageName = imageNameInputField.text;
        if (string.IsNullOrEmpty(imageName))
        {
            Debug.Log("please enter image name");
            return;
        }
        string imagePath = UnityEditor.EditorUtility.OpenFilePanel("select image", "", "png, jpg, jpeg");
        if (string.IsNullOrEmpty(imagePath))
        {
            Debug.Log("no image selected");
            return;
        }

        Debug.Log(imagePath);
        byte[] imageBytes = File.ReadAllBytes(imagePath);

        StorageReference imageRef = storageReference.Child(imageName);
        imageRef.PutBytesAsync(imageBytes).ContinueWithOnMainThread(t =>
        {
            if (t.IsCompleted && !t.IsFaulted && !t.IsCanceled)
            {
                Debug.Log("image upload complete");
                imageRef.GetDownloadUrlAsync().ContinueWithOnMainThread(downloadTask =>
                {
                    if (downloadTask.IsCompleted && !downloadTask.IsFaulted && !downloadTask.IsCanceled)
                    {
                        uploadedUrl = downloadTask.Result.ToString();
                        DocumentReference imageInfoRef = firestore.Collection("image").Document(imageName);
                        var data = new Dictionary<string, object>
                        {
                            {"imageName", imageName }, {"imageUrl", uploadedUrl}
                        };
                        Debug.Log("image upload");
                        imageInfoRef.SetAsync(data).ContinueWithOnMainThread(setTask =>
                        {
                            if (setTask.IsCompleted && !setTask.IsFaulted && !setTask.IsCanceled)
                            {
                                Debug.Log("image info added to firestore");
                            }
                            else
                            {
                                Debug.Log("failed to add image");
                            }
                        });
                    }
                });
            }
        });
    }
}
