using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    List<Bullet> bullets = new List<Bullet>();
    Queue<Bullet> bulletsQueue = new Queue<Bullet>();
    Stack<Bullet> bulletsStack = new Stack<Bullet>();

    // Bullet Pool에서 사용할 수 있는 총알을 반환하는 메서드
    public Bullet GetBullet()
    {
        // if (bullets.Count > 0)
        // {
        //     Bullet bullet = bullets[0];
        //     bullets.RemoveAt(0);
        //     bullet.gameObject.SetActive(true);

        //     return bullet;
        // }

        if (bulletsQueue.Count > 0)
        {
            Bullet bullet = bulletsQueue.Dequeue();
            bullet.gameObject.SetActive(true);

            return bullet;
        }

        return null;
    }

    // 더이상 사용되지 않는 총알을 Bullet Pool에 추가하는 메서드
    public void AddBullet(Bullet bullet)
    {
        //bullets.Add(bullet);
        bulletsQueue.Enqueue(bullet);
    }
}
