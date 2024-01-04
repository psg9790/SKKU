using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class TAGO_WebRequest : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] Transform content;

    private void Start()
    {
        StartCoroutine(TAGO_ShipListRequest());
    }

    IEnumerator TAGO_ShipListRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://apis.data.go.kr/1613000/DmstcShipNvgInfoService/getShipOpratInfoList?serviceKey=3t8heetsrzhDJSU1UfM8V6q6pH28VXzklod%2FbQMtbHbS8UijuhH%2BPiywBIzME1nG2xAFXxiMLDY3NQtmqo1NZQ%3D%3D&depPlandTime=20230403&depNodeId=SEA96140&_type=json");

        // 전송완료 후 값이 올때까지 기다림
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Container container = JsonUtility.FromJson<Container>(www.downloadHandler.text);

            foreach (var item in container.response.body.items.item)
            {
                GameObject go = Instantiate(cellPrefab, content);
                TAGO_Cell cell = go.GetComponent<TAGO_Cell>();
                cell.SetItem(item);
            }
        }
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
