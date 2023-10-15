using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : TankBase
{
    public float fireOffsetTime = 1f;
    public Transform[] firePosition;
    public GameObject bulletPrefab;
    private float time = 0;
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= fireOffsetTime)
        {
            Fire();
            time = 0;
        }
        head.Rotate(Vector3.up * headRotateSpeed * Time.deltaTime);
    }
    public override void Fire()
    {
        if (firePosition != null && bulletPrefab != null)
        {
            for (int i = 0; i < firePosition.Length; i++)
            {
                Bullet bullet = Instantiate(bulletPrefab, firePosition[i].position, firePosition[i].rotation).GetComponent<Bullet>();
                bullet.SetGrandFather(this);
            }
        }
    }
    public override void Hurt(TankBase other)
    {
        
    }
}
