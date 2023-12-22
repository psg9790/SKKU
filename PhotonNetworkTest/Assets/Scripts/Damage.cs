using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Player = Photon.Realtime.Player;

public class Damage : MonoBehaviour
{
    // 투명 처리를 위한 MeshRenderer 컴포넌트 배열
    Renderer[] renderers;

    //초기 HP
    int iniHp = 100;

    //현재 HP
    public int currentHP = 100;
    Animator anim;
    CharacterController cc;

    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashRespawn = Animator.StringToHash("Respawn");

    // 컴포넌트들을 Awake 함수를 써서 가져옴
    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        currentHP = iniHp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //HP가 양수이고, 충돌체의 태그가 BULLET인 경우 HP 차감
        if (currentHP > 0 && collision.collider.CompareTag("BULLET")) //부딫힌 애의 태그가 "~"라면
        {
            currentHP -= 15;
            //HP가 음수면 Die 코루틴 실행
            if (currentHP < 0)
            {
                StartCoroutine(PlayerDie());
            }
            Debug.Log("HP: " + currentHP);
        }
    }

    IEnumerator PlayerDie()
    {
        //캐릭터 컨트롤러 컴포넌트 비활성화
        cc.enabled = false;
        //리스폰 애니메이션 비활성화
        anim.SetBool(hashRespawn, false);
        //사망 애니메이션 활성화
        anim.SetTrigger(hashDie);

        //대기
        yield return new WaitForSeconds(3f);
        //리스폰 활성화
        anim.SetBool(hashRespawn, true);
        //캐릭터 렌더러 비활성화 함수
        SetPlayerVisible(false);

        //대기 
        yield return new WaitForSeconds(3f);
        //체력 재설정
        currentHP = 100;
        //캐릭터 렌더러 활성화
        SetPlayerVisible(true);
        // 캐릭터 컨트롤러 활성화
        cc.enabled = true;
    }
    // 캐릭터 렌더러 컴포넌트 배열을 활성화/비활성화 하는 함수
    void SetPlayerVisible(bool isVisible)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            //렌더러 배열을 활성화 할지 안할지 여부
            renderers[i].enabled = isVisible; //파라메터로 받아옴
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}