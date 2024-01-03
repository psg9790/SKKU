using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject bulletFactory;
    [SerializeField] BulletPool bulletPool;

    Stack<int> sss;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // GameObject go = Instantiate(bulletFactory, transform.position, transform.rotation);
            // Bullet bullet = go.GetComponent<Bullet>();
            // bullet.Shoot();

            Bullet bullet = bulletPool.GetBullet();
            if (bullet == null)
            {
                bullet = Instantiate(bulletFactory, transform.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.bulletPool = bulletPool;
            }
            bullet.Shoot();
        }
    }
}
