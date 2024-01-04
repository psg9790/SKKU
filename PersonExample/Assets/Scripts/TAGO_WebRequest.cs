using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class TAGO_WebRequest : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] Transform content;

    [SerializeField] Text pageDisplayText;
    private int pageIndex = 1;
    private int minIndex = 1;
    private int maxIndex = 5;

    private void Start()
    {
        StartCoroutine(TAGO_ShipListRequest());
    }

    IEnumerator TAGO_ShipListRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get($"http://apis.data.go.kr/1613000/DmstcShipNvgInfoService/getShipOpratInfoList?" +
        $"serviceKey=3t8heetsrzhDJSU1UfM8V6q6pH28VXzklod%2FbQMtbHbS8UijuhH%2BPiywBIzME1nG2xAFXxiMLDY3NQtmqo1NZQ%3D%3D&" +
        $"depPlandTime=20230403&" +
        $"depNodeId=SEA96140&" +
        $"_type=json&" +
        $"pageNo={pageIndex}");

        // 전송완료 후 값이 올때까지 기다림
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Container container = JsonUtility.FromJson<Container>(www.downloadHandler.text);

            pageDisplayText.text = $"page {container.response.body.pageNo} / {Mathf.Ceil(container.response.body.totalCount / container.response.body.numOfRows)}";
            maxIndex = (int)Mathf.Ceil(container.response.body.totalCount / container.response.body.numOfRows);

            foreach (var item in container.response.body.items.item)
            {
                GameObject go = Instantiate(cellPrefab, content);
                TAGO_Cell cell = go.GetComponent<TAGO_Cell>();
                cell.SetItem(item);
            }
        }
    }

    public void NextButtonClick()
    {
        ClearContent();
        AddPageIndex(+1);
        StartCoroutine(TAGO_ShipListRequest());
    }

    public void PrevButtonClick()
    {
        ClearContent();
        AddPageIndex(-1);
        StartCoroutine(TAGO_ShipListRequest());
    }

    void ClearContent()
    {
        Transform[] childs = content.GetComponentsInChildren<Transform>();

        for (int i = 1; i < childs.Length; i++)
            Destroy(childs[i].gameObject);
    }

    void AddPageIndex(int amount)
    {
        pageIndex += amount;
        pageIndex = Mathf.Clamp(pageIndex, 1, 5);
    }
}

[Serializable]
class Container
{
    public Response response;
}

[Serializable]
class Response
{
    public Header header;
    public Body body;
}

[Serializable]
class Header
{
    public string resultCode;
    public string resultMsg;
}

[Serializable]
class Body
{
    public Items items;
    public int numOfRows;
    public int pageNo;
    public int totalCount;
}
[Serializable]
public class Items
{
    public Item[] item;
}

[Serializable]
public class Item
{
    public string arrPlaceNm;
    public long arrPlandTime;
    public int charge;
    public string depPlaceNm;
    public long depPlandTime;
    public string vihicleNm;
}
