using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        // 총알 발사
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000f);
        // 3초 뒤 총알 삭제
        Destroy(this.gameObject, 3);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌 지점 추출
        var contact  = collision.GetContact(0);
        // 충돌 지점에 이펙트 생성
        var obj = Instantiate(effect, contact.point, Quaternion.LookRotation(-contact.normal));
        // 이펙트 제거
        Destroy(obj, 2);
        // 총알 제거
        Destroy(this.gameObject);
    }
}
