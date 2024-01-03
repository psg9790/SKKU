using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySort : MonoBehaviour
{
    void Start()
    {
        int[] scores = { 10, 30, 60, 20, 80, 50, 90, 70, 40, 35 };
        // 내가 구현한 sort (선택정렬)
        int[] newScores = MySort(scores);

        // 원본 배열 출력
        string out1 = "scores: ";
        foreach (int val in scores)
            out1 += val.ToString() + " ";
        Debug.Log(out1);

        // 정렬한 배열 출력
        string out2 = "newScores: ";
        foreach (int val in newScores)
            out2 += val.ToString() + " ";
        Debug.Log(out2);

        // 내가 구현한 이진탐색
        int target = 70;
        int result = MyBinarySearch(newScores, target);
        Debug.Log($"{target} is at: {result}");

    }



    // 내가 구현한 이진탐색
    int MyBinarySearch(int[] scores, int findValue)
    {
        int lft = 0, rgt = scores.Length - 1;

        while (lft <= rgt)
        {
            int mid = (lft + rgt) / 2;

            // 오른쪽 구역으로
            if (scores[mid] < findValue)
            {
                lft = mid + 1;
            }
            // 왼쪽 구역으로
            else if (scores[mid] > findValue)
            {
                rgt = mid - 1;
            }
            // 찾음
            else
            {
                return mid;
            }
        }
        return -1;
    }

    // 내가 구현한 선택 정렬
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
