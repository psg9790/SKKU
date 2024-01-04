using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingHashTable : MonoBehaviour
{
    void Start()
    {
        Hashtable ht = new Hashtable();
        ht["하나"] = "one";
        ht["둘"] = "two";
        ht["셋"] = "three";
        ht["넷"] = "four";
        ht["다섯"] = "five";

        Debug.Log(ht["하나"]);
        Debug.Log(ht["둘"]);
        Debug.Log(ht["셋"]);
        Debug.Log(ht["넷"]);
        Debug.Log(ht["다섯"]);
    }
}
