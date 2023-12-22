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
        // ����Ʈ �׷� �迭�� �޾ƿ�. ����Ʈ �׷��� �ڽĵ��� Ʈ������ ������Ʈ�� �޾ƿ�.
        Transform[] points = GameObject.Find("PointGroup").GetComponentsInChildren<Transform>();
        // 1���� �迭�� ���̱����� ���� �� ������ ��(idx) ����
        int idx = Random.Range(1, points.Length);
        // �÷��̾� �������� ������ idx ��ġ�� ȸ�� ���� ����
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
