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
        // 매개변수의 배열은 참조변수이므로 값 수정 시 원본도 함께 바뀜,
        // 새로운 배열에 값들을 복사한 후 정렬해야 함
        int[] newScore = new int[scores.Length];
        Array.Copy(scores, newScore, scores.Length);

        // 앞에거 i
        for (int i = 0; i <= newScore.Length - 2; i++)
        {
            // 뒤에거 j
            for (int j = i + 1; j <= newScore.Length - 1; j++)
            {
                // 작은게 뒤에 있으면 swap
                if (newScore[i] > newScore[j])
                {
                    // swap
                    int temp = newScore[j];
                    newScore[j] = newScore[i];
                    newScore[i] = temp;
                }
            }
        }

        return newScore;
    }
}
