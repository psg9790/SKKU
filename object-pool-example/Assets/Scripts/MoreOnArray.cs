using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreOnArray : MonoBehaviour
{
    bool CheckPassed(int score)
    {
        return score >= 60;
    }

    void Print(int value)
    {
        Debug.Log($"{value}");
    }

    void Start()
    {
        int[] scores = new int[] { 80, 74, 81, 90, 34 };

        foreach (int score in scores)
            Debug.Log($"{score}");

        Debug.Log("---");

        Array.Sort(scores);
        Array.ForEach<int>(scores, new Action<int>(Print));

        Debug.Log("---");

        Debug.Log($"Number of dimensions : {scores.Rank}");     // 배열의 차원

        Debug.Log($"Binary search : 81 is at {Array.BinarySearch<int>(scores, 81)}");   // 이분 탐색

        Debug.Log($"Linear search : 81 is at {Array.IndexOf<int>(scores, 81)}");        // 순차적으로 탐색

        Debug.Log($"Everyone passed : {Array.TrueForAll<int>(scores, CheckPassed)}");   // 모든 요소가 조건에 맞는지 확인

        int index = Array.FindIndex<int>(scores, (score) => score < 60);                // 조건에 맞지 않는 요소 찾기
        scores[index] = 61;
        Debug.Log($"Everyone passed : {Array.TrueForAll<int>(scores, CheckPassed)}");   // 모든 요소가 조건에 맞는지 확인

        Debug.Log($"Old length of scores : {scores.GetLength(0)}");     // 배열의 길이

        Array.Resize<int>(ref scores, 10);
        Debug.Log($"New length of scores : {scores.Length}");

        Array.ForEach<int>(scores, new Action<int>(Print));

        Array.Clear(scores, 3, 7);
        Array.ForEach<int>(scores, new Action<int>(Print));

        int[] sliced = new int[3];
        Array.Copy(scores, 0, sliced, 0, 3);
        Array.ForEach<int>(sliced, new Action<int>(Print));

    }
}
