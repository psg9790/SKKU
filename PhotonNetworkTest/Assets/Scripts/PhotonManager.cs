using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "1.0";
    private string userId = "psg9790";

    private void Awake()
    {
        // 씬 동기화
        PhotonNetwork.AutomaticallySyncScene = true;
        // 버전 할당
        PhotonNetwork.GameVersion = version;
        // App ID 
        PhotonNetwork.NickName = userId;
        // 포톤 서버와의 통신 횟수 (기본값: 30)
        Debug.Log($"PhotonNetwork.SendRate: {PhotonNetwork.SendRate}");
        // 포톤 서버 접속 (Name Server)
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"In Lobby = {PhotonNetwork.InLobby}");   // false
        // 로비 접속
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"In Lobby = {PhotonNetwork.InLobby}");   // true
        // 방 접속. 1) 랜덤 매치메이킹 2) 커스텀된 방
        
        // 1) 랜덤 매치메이킹
        PhotonNetwork.JoinRandomRoom();
    }
    // 1-1) 룸이 생성되지 않았으면 오류 콜백 함수 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Failed {returnCode}: {message}");

        // 룸 속성 설정
        RoomOptions ro = new RoomOptions();
        // 룸에 접속할 수 있는 최대 접속자 수
        ro.MaxPlayers = 20;
        // 룸 오픈 여부
        ro.IsOpen = true;
        // 로비에서 룸 목록에 노출시킬지 여부
        ro.IsVisible = true;
        // 룸 생성
        PhotonNetwork.CreateRoom("PSG Room", ro);
    }
    // 1-2) 룸이 생성되었을 때
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room!");
        // 방이 생성되면 자동으로 입장됨
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"In Room = {PhotonNetwork.InRoom}");
        // 방에 들어와있는 사람 수
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");
        // 접속한 사용자 닉네임 확인
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            // 플레이어 닉네임, 고유 값
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");
        }

        // 포인트 그룹 배열을 받아옴. 포인트 그룹의 자식들의 트랜스폼 컴포넌트를 받아옴.
        Transform[] points = GameObject.Find("PointGroup").GetComponentsInChildren<Transform>();
        // 1부터 배열의 길이까지의 숫자 중 랜덤한 값(idx) 추출
        int idx = Random.Range(1, points.Length);
        // 플레이어 프리팹을 추출한 idx 위치와 회전 값에 생성
        PhotonNetwork.Instantiate("Player", points[idx].position, points[idx].rotation, 0);
    }
}
