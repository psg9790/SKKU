using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 깜박하고 rigidbody를 추가해놓지 않아도 자동으로 추가해줌
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public BulletPool bulletPool;   // 오브젝트풀링 pool

    float force = 1000.0f;  // 쏘는 속도
    Rigidbody rb;
    float killTime = 3;     // 총알 지속 시간
    float time = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();     // rigidbody 초기화
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > killTime)    // 일정 시간이 지나면 비활성화
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Shoot()
    {
        rb.AddForce(transform.forward * force);     // 앞방향으로 물리적 힘 가함
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);   // 트리거가 발동되면 비활성화
    }

    void OnEnable()
    {
        this.transform.position = Vector3.zero;     // 활성화되면 다시 원점위치로 초기화해줌
    }

    void OnDisable()
    {
        this.time = 0;  // 비활성화 타이머 초기화
        rb.velocity = Vector3.zero; // rigidbody 물리 속도를 0으로 죽임: 비활성화된건 떨어지던 상태이기 때문에 (관성포함)

        bulletPool.AddBullet(this);     // 오브젝트풀링
    }
}
