using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionarySample : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject cellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, string> person1 = new Dictionary<string, string>();
        person1["Name"] = "홍길동";
        person1["Phone"] = "010-1234-5678";
        person1["Address"] = "경기도 안양시 동안구";

        Dictionary<string, string> person2 = new Dictionary<string, string>();
        person2["Name"] = "김길동";
        person2["Phone"] = "010-1234-0001";
        person2["Address"] = "경기도 부천시 평천로";

        Dictionary<string, string> person3 = new Dictionary<string, string>();
        person3["Name"] = "최길동";
        person3["Phone"] = "010-1234-0002";
        person3["Address"] = "경기도 성남시 분당구";

        List<Dictionary<string, string>> addressBook = new List<Dictionary<string, string>>();
        addressBook.Add(person1);
        addressBook.Add(person2);
        addressBook.Add(person3);

        // 불러오기
        Dictionary<string, string> dict = addressBook[0];
        foreach (var val in dict)
        {
            Debug.Log(val.Value);
        }

        // Cell 만들기
        foreach (var val in addressBook)
        {
            GameObject cellObject = Instantiate(cellPrefab, content);
            CellController cc = cellObject.GetComponent<CellController>();
            cc.SetData(val);
        }

    }
}
