using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Player = Photon.Realtime.Player;

public class Damage : MonoBehaviour
{
    // ���� ó���� ���� MeshRenderer ������Ʈ �迭
    Renderer[] renderers;

    //�ʱ� HP
    int iniHp = 100;

    //���� HP
    public int currentHP = 100;
    Animator anim;
    CharacterController cc;

    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashRespawn = Animator.StringToHash("Respawn");

    // ������Ʈ���� Awake �Լ��� �Ἥ ������
    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        currentHP = iniHp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //HP�� ����̰�, �浹ü�� �±װ� BULLET�� ��� HP ����
        if (currentHP > 0 && collision.collider.CompareTag("BULLET")) //�΋H�� ���� �±װ� "~"���
        {
            currentHP -= 15;
            //HP�� ������ Die �ڷ�ƾ ����
            if (currentHP < 0)
            {
                StartCoroutine(PlayerDie());
            }
            Debug.Log("HP: " + currentHP);
        }
    }

    IEnumerator PlayerDie()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ ��Ȱ��ȭ
        cc.enabled = false;
        //������ �ִϸ��̼� ��Ȱ��ȭ
        anim.SetBool(hashRespawn, false);
        //��� �ִϸ��̼� Ȱ��ȭ
        anim.SetTrigger(hashDie);

        //���
        yield return new WaitForSeconds(3f);
        //������ Ȱ��ȭ
        anim.SetBool(hashRespawn, true);
        //ĳ���� ������ ��Ȱ��ȭ �Լ�
        SetPlayerVisible(false);

        //��� 
        yield return new WaitForSeconds(3f);
        //ü�� �缳��
        currentHP = 100;
        //ĳ���� ������ Ȱ��ȭ
        SetPlayerVisible(true);
        // ĳ���� ��Ʈ�ѷ� Ȱ��ȭ
        cc.enabled = true;
    }
    // ĳ���� ������ ������Ʈ �迭�� Ȱ��ȭ/��Ȱ��ȭ �ϴ� �Լ�
    void SetPlayerVisible(bool isVisible)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            //������ �迭�� Ȱ��ȭ ���� ������ ����
            renderers[i].enabled = isVisible; //�Ķ���ͷ� �޾ƿ�
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}