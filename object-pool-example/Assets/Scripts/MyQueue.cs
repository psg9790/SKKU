using System.Collections;
using System.Collections.Generic;

public class MyQueue<T>
{
    List<T> values = new List<T>();

    // 큐에 저장된 요소의 수
    public int Count => values.Count;

    /// <summary>
    /// 값 추가
    /// </summary>
    /// <param name="value">값</param>
    public void Enqueue(T value)
    {
        values.Add(value);
    }

    /// <summary>
    /// 값 추출
    /// </summary>
    /// <returns>값</returns>
    public T Dequeue()
    {
        // 리스트 맨앞 값 리턴
        // 큐: FIFO
        int lastIndex = 0;
        T lastValue = values[lastIndex];
        values.RemoveAt(lastIndex);

        return lastValue;
    }
}
