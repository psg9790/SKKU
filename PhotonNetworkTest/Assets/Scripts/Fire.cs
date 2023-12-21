using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // �ѱ� ��ġ
    public Transform firePos;
    // �Ѿ� ������
    public GameObject bulletPrefab;
    // ��ƼŬ �ý���
    ParticleSystem muzzleFlash;
    // ���� �� ������Ʈ
    PhotonView pv;
    // ���콺 ���� Ŭ�� �̺�Ʈ
    bool isMouseClick => Input.GetMouseButtonDown(0);

    void Start()
    {
        pv = GetComponent<PhotonView>();
        muzzleFlash = firePos.Find("MuzzleFlash").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���߰�, ���� ������� �Ѿ� �߻�
        if(isMouseClick && pv.IsMine)
        {
            FireBullet(pv.Owner.ActorNumber);
            // RPC�� �������� �ִ� �Լ� ȣ��
            pv.RPC("FireBullet", RpcTarget.Others, pv.Owner.ActorNumber);
        }
    }

    [PunRPC]
    void FireBullet(int actorNo)
    {
        // �ѱ� ȭ��ȿ���� ���� ���� �ƴ� �� ȿ�� ����
        if(!muzzleFlash.isPlaying)
        {
            muzzleFlash.Play(true);
        }
        // �Ѿ� ����
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}
