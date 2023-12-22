using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        CreatePlayer();    
    }

    void CreatePlayer()
    {
        // 포인트 그룹 배열을 받아옴. 포인트 그룹의 자식들의 트랜스폼 컴포넌트를 받아옴.
        Transform[] points = GameObject.Find("PointGroup").GetComponentsInChildren<Transform>();
        // 1부터 배열의 길이까지의 숫자 중 랜덤한 값(idx) 추출
        int idx = Random.Range(1, points.Length);
        // 플레이어 프리팹을 추출한 idx 위치와 회전 값에 생성
        PhotonNetwork.Instantiate("Player", points[idx].position, points[idx].rotation, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
