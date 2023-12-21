using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // 총구 위치
    public Transform firePos;
    // 총알 프리팹
    public GameObject bulletPrefab;
    // 파티클 시스템
    ParticleSystem muzzleFlash;
    // 포톤 뷰 컴포넌트
    PhotonView pv;
    // 마우스 왼쪽 클릭 이벤트
    bool isMouseClick => Input.GetMouseButtonDown(0);

    void Start()
    {
        pv = GetComponent<PhotonView>();
        muzzleFlash = firePos.Find("MuzzleFlash").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼을 클릭했고, 로컬 유저라면 총알 발사
        if(isMouseClick && pv.IsMine)
        {
            FireBullet(pv.Owner.ActorNumber);
            // RPC로 원격지에 있는 함수 호출
            pv.RPC("FireBullet", RpcTarget.Others, pv.Owner.ActorNumber);
        }
    }

    [PunRPC]
    void FireBullet(int actorNo)
    {
        // 총구 화염효과가 실행 중이 아닐 때 효과 실행
        if(!muzzleFlash.isPlaying)
        {
            muzzleFlash.Play(true);
        }
        // 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}
