using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Person : IComparable<Person>, IEquatable<Person>  // 인터페이스 상속
{
    public string Name { get; private set; }
    public int Age { get; private set; }
    public string Id { get; private set; }

    public Person(string name, int age, string id)
    {
        this.Name = name;
        this.Age = age;
        this.Id = id;
    }

    // 비교 연산자를 정의할 수 있음!
    // 여기서는 나이로 비교
    public int CompareTo(Person other)
    {
        if (other == null)
            return 1;
        else
            return this.Age.CompareTo(other.Age);
    }

    // IEquatable의 Equals를 재정의
    // 객체에서 EqualsTo 메서드를 사용하면 다음과 같은 기준으로 비교
    public bool Equals(Person other)
    {
        if (other == null)
            return false;
        return Equals(this.Id, other.Id);
    }
}
public class MoreArraySort : MonoBehaviour
{
    void Start()
    {
        Person p1 = new Person("홍길동", 93, "001");
        Person p2 = new Person("김동길", 83, "002");
        Person p3 = new Person("동김길", 63, "003");
        Person p4 = new Person("최길동", 53, "004");
        Person p5 = new Person("흐동길", 23, "005");
        Person p6 = new Person("미동길", 73, "006");
        Person p7 = new Person("무길동", 33, "007");
        Person p8 = new Person("허동길", 13, "008");
        Person p9 = new Person("길길길", 43, "009");
        List<Person> list = new List<Person> { p1, p2, p3, p4, p5, p6, p7, p8, p9 };

        // 이진탐색을 위해 나이순으로 정렬
        list.Sort();    // 비교자는 클래스 안에 있음
        int target = 63;
        int result = BinaryAgeSearch(list.ToArray(), target);
        Debug.Log($"{target}살인 사람은 {result}번째 / 이름: {list[result].Name}, ID: {list[result].Id}");

        // 이진탐색을 위해 이름순으로 정렬
        list.Sort(new Comparison<Person>((n1, n2) => n1.Name.CompareTo(n2.Name)));  // 람다식으로 비교자 대입
        string target2 = "미동길";
        int result2 = BinaryNameSearch(list.ToArray(), target2, 0, list.Count - 1);
        Debug.Log($"이름이 {target2}인 사람은 {result2}번째 / 나이: {list[result2].Age}, ID: {list[result2].Id}");

        Person p10 = new Person("구구구", 99, "999");
        if (p6.Equals(p10))
        {
            Debug.Log("같음");
        }
        else
        {
            Debug.Log("다름");
        }
    }

    /// <summary>
    /// 나이로 사람찾기 (이진탐색)
    /// </summary>
    /// <param name="persons">Person 배열 (정렬된)</param>
    /// <param name="age">찾을 나이</param>
    /// <returns>int (null: -1)</returns>
    int BinaryAgeSearch(Person[] persons, int age)
    {
        int low = 0, high = persons.Length - 1;
        int mid;

        while (low <= high)
        {
            mid = low + (high - low) / 2;

            if (persons[mid].Age == age)
            {
                return mid;
            }
            else if (persons[mid].Age > age)
            {
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }
        return -1;
    }

    /// <summary>
    /// 이름으로 사람찾기 (이진탐색)
    /// </summary>
    /// <param name="persons">Person 배열 (정렬된)</param>
    /// <param name="name">찾을 이름</param>
    /// <returns>string</returns>
    int BinaryNameSearch(Person[] persons, string name, int v_low, int v_high)
    {
        if (v_low > v_high)
            return -1;
        if (name == null)
            return -1;

        int mid = (v_low + v_high) / 2;

        if (persons[mid].Name.CompareTo(name) == 0)
        {
            return mid;
        }
        else if (persons[mid].Name.CompareTo(name) > 0)
        {
            v_high = mid - 1;
            return BinaryNameSearch(persons, name, v_low, v_high);
        }
        else
        {
            v_low = mid + 1;
            return BinaryNameSearch(persons, name, v_low, v_high);
        }
    }
}
