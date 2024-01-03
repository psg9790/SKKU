using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySample : MonoBehaviour
{
    private void Start()
    {
        int[] scores = new int[5];
        scores[0] = 80;
        scores[1] = 72;
        scores[2] = 81;
        scores[3] = 90;
        scores[4] = 34;

        foreach (int i in scores)
            Debug.Log(i);

        int sum = 0;

        foreach (int i in scores)
            sum += i;

        int average = sum / scores.Length;

        Debug.Log($"Average score: {average}");

        string[] array1 = new string[3] { "안녕", "Hello", "Halo" };
        string[] array2 = new string[] { "안녕", "Hello", "Halo" };
        string[] array3 = { "안녕", "Hello", "Halo" };
    }
}
