using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackQueueExample : MonoBehaviour
{
    private void Start()
    {
        MyStack<int> stack = new MyStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        stack.Push(5);
        stack.Push(6);
        while (stack.Count > 0)
        {
            int result = stack.Pop();
            Debug.Log($"Pop: {result}");
        }

        try
        {
            stack.Pop();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        MyQueue<int> queue = new MyQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);
        while (queue.Count > 0)
        {
            int result = queue.Dequeue();
            Debug.Log($"Dequeue: {result}");
        }
    }
}
