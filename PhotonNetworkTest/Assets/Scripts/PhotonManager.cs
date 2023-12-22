using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "1.0";
    private string userId = "psg9790";

    // ���� ID�� �Է��� ��ǲ �ʵ�
    public TMP_InputField userIF;
    // �� ID�� �Է��� ��ǲ �ʵ�
    public TMP_InputField roomIF;

    private void Awake()
    {
        // �� ����ȭ
        PhotonNetwork.AutomaticallySyncScene = true;
        // ���� �Ҵ�
        PhotonNetwork.GameVersion = version;
        // App ID 
        PhotonNetwork.NickName = userId;
        // ���� �������� ��� Ƚ�� (�⺻��: 30)
        Debug.Log($"PhotonNetwork.SendRate: {PhotonNetwork.SendRate}");
        // ���� ���� ���� (Name Server)
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"In Lobby = {PhotonNetwork.InLobby}");   // false
        // �κ� ����
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"In Lobby = {PhotonNetwork.InLobby}");   // true
        // �� ����. 1) ���� ��ġ����ŷ 2) Ŀ���ҵ� ��
        
        // 1) ���� ��ġ����ŷ
        //PhotonNetwork.JoinRandomRoom();
    
        // 2) ����
    }

    // 1-1) ���� �������� �ʾ����� ���� �ݹ� �Լ� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Failed {returnCode}: {message}");

        OnMakeRoomClick();

        /*// �� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        // �뿡 ������ �� �ִ� �ִ� ������ ��
        ro.MaxPlayers = 20;
        // �� ���� ����
        ro.IsOpen = true;
        // �κ񿡼� �� ��Ͽ� �����ų�� ����
        ro.IsVisible = true;
        // �� ����
        PhotonNetwork.CreateRoom("PSG Room", ro);*/
    }

    // 1-2) ���� �����Ǿ��� ��
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room!");
        // ���� �����Ǹ� �ڵ����� �����
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"In Room = {PhotonNetwork.InRoom}");
        // �濡 �����ִ� ��� ��
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");
        // ������ ����� �г��� Ȯ��
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            // �÷��̾� �г���, ���� ��
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");
        }

        /*// ����Ʈ �׷� �迭�� �޾ƿ�. ����Ʈ �׷��� �ڽĵ��� Ʈ������ ������Ʈ�� �޾ƿ�.
        Transform[] points = GameObject.Find("PointGroup").GetComponentsInChildren<Transform>();
        // 1���� �迭�� ���̱����� ���� �� ������ ��(idx) ����
        int idx = Random.Range(1, points.Length);
        // �÷��̾� �������� ������ idx ��ġ�� ȸ�� ���� ����
        PhotonNetwork.Instantiate("Player", points[idx].position, points[idx].rotation, 0);*/

        // ������ Ŭ���̾�Ʈ�� ��� ���� �� �ε�
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    private void Start()
    {
        // ���� ID ���� ����
        userId = PlayerPrefs.GetString("USER_ID", $"USER_{Random.Range(1, 21):00}");
        userIF.text = userId;
        // ���� �г��� ���
        PhotonNetwork.NickName = userId;
    }

    // �������� �����ϴ� ����
    public void SetUserId()
    {
        if(string.IsNullOrEmpty(userIF.text))
        {
            userId = $"USER_{Random.Range(1, 21):00}";
        }
        else
        {
            userId = userIF.text;
        }
        // ������ ����
        PlayerPrefs.SetString("USER_ID", userId);
        PhotonNetwork.NickName = userId;
    }

    string SetRoomName()
    {
        if(string.IsNullOrEmpty(roomIF.text))
        {
            roomIF.text = $"ROOM_{Random.Range(1, 101):000}";
        }
        return roomIF.text;
    }

    public void OnLoginClick()
    {
        // ���� ID ����
        SetUserId();
        // ������ ������ ����
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnMakeRoomClick()
    {
        // ���� ID ����
        SetUserId();
        // �� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        ro.IsOpen = true;
        ro.IsVisible = true;
        // �� ����
        PhotonNetwork.CreateRoom(SetRoomName(), ro);
    }

}
