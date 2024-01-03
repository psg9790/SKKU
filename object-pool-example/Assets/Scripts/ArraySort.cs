using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySort : MonoBehaviour
{
    void Start()
    {
        int[] scores = { 10, 30, 60, 20, 80, 50, 90, 70, 40, 35 };
        int[] newScores = MySort(scores);

        string out1 = "scores: ";
        foreach (int val in scores)
            out1 += val.ToString() + " ";
        Debug.Log(out1);

        string out2 = "newScores: ";
        foreach (int val in newScores)
            out2 += val.ToString() + " ";
        Debug.Log(out2);
    }

    int[] MySort(int[] scores)
    {
        int[] newScore = new int[scores.Length];
        Array.Copy(scores, newScore, scores.Length);

        for (int i = 0; i <= newScore.Length - 2; i++)
        {
            for (int j = i + 1; j <= newScore.Length - 1; j++)
            {
                if (newScore[i] > newScore[j])
                {
                    int temp = newScore[j];
                    newScore[j] = newScore[i];
                    newScore[i] = temp;
                }
            }
        }

        return newScore;
    }
}
