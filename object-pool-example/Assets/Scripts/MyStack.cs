using System.Collections;
using System.Collections.Generic;

public class MyStack<T>
{
    List<T> values = new List<T>();

    // 스택에 저장된 요소의 수
    public int Count => values.Count;

    /// <summary>
    /// 값 추가
    /// </summary>
    /// <param name="value">값</param>
    public void Push(T value)
    {
        values.Add(value);
    }

    /// <summary>
    /// 값 추출
    /// </summary>
    /// <returns>값</returns>
    public T Pop()
    {
        // 리스트 맨 마지막 값 리턴
        // 스택: FILO
        if (values.Count > 0)
        {
            int lastIndex = values.Count - 1;
            T lastValue = values[lastIndex];
            values.RemoveAt(lastIndex);
            return lastValue;
        }
        else
        {
            throw new System.Exception("스택 비어있음");
        }
    }
}
