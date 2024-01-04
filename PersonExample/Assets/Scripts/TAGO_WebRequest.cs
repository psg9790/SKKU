using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class TAGO_WebRequest : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab; // UI 블록 프리팹
    [SerializeField] Transform content;     // UI 블록들을 생성할 transform

    [SerializeField] Text pageDisplayText;  // 현재 페이지 숫자를 표시할 텍스트
    private int pageIndex = 1;              // 현재 페이지 숫자
    private int minIndex = 1;               // 최소 페이지 숫자
    private int maxIndex = 5;               // 최대 페이지 숫자

    private void Start()
    {
        StartCoroutine(TAGO_ShipListRequest());
    }

    /// <summary>
    /// json 정보 불어오는 코루틴
    /// </summary>
    /// <returns></returns>
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

        // 에러가 없으면
        if (www.error == null)
        {
            // 현재 UI 블록들을 삭제
            ClearContent();

            // json 파일을 Container 클래스로 변환
            Container container = JsonUtility.FromJson<Container>(www.downloadHandler.text);

            // 최대 페이지 수
            maxIndex = (int)Mathf.Ceil(container.response.body.totalCount / container.response.body.numOfRows);
            // 현재 페이지 숫자 표시
            pageDisplayText.text = $"page {container.response.body.pageNo} / {maxIndex}";

            // 변환된 데이터 안의 아이템들의 정보로 UI 생성
            foreach (var item in container.response.body.items.item)
            {
                GameObject go = Instantiate(cellPrefab, content);
                TAGO_Cell cell = go.GetComponent<TAGO_Cell>();
                cell.SetItem(item);
            }
        }
    }

    /// <summary>
    /// next 버튼 클릭 시 (다음 json 페이지 로드)
    /// </summary>
    public void NextButtonClick()
    {
        AddPageIndex(+1);
        StartCoroutine(TAGO_ShipListRequest());
    }

    /// <summary>
    /// prev 버튼 클릭 시 (이전 json 페이지 로드)
    /// </summary>
    public void PrevButtonClick()
    {
        AddPageIndex(-1);
        StartCoroutine(TAGO_ShipListRequest());
    }

    /// <summary>
    /// 현재 생성된 UI 블록들을 삭제
    /// </summary>
    void ClearContent()
    {
        Transform[] childs = content.GetComponentsInChildren<Transform>();

        for (int i = 1; i < childs.Length; i++)
            Destroy(childs[i].gameObject);
    }

    /// <summary>
    /// 페이지 이동
    /// </summary>
    /// <param name="amount">페이지 이동 수</param>
    void AddPageIndex(int amount)
    {
        pageIndex += amount;
        pageIndex = Mathf.Clamp(pageIndex, 1, 5);
    }
}

#region json => class
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
#endregion
