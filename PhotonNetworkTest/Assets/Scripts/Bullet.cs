using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        // �Ѿ� �߻�
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000f);
        // 3�� �� �Ѿ� ����
        Destroy(this.gameObject, 3);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // �浹 ���� ����
        var contact  = collision.GetContact(0);
        // �浹 ������ ����Ʈ ����
        var obj = Instantiate(effect, contact.point, Quaternion.LookRotation(-contact.normal));
        // ����Ʈ ����
        Destroy(obj, 2);
        // �Ѿ� ����
        Destroy(this.gameObject);
    }
}
