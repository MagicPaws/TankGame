using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : TankBase
{
    // 攻击偏移时间
    public float fireOffsetTime = 1f;
    // 攻击位置
    public Transform[] firePosition;
    // 子弹预设体
    public GameObject bulletPrefab;
    // 攻击偏移时间计数
    private float time = 0;
    private void Update()
    {
        // 每次更新增加的时间
        time += Time.deltaTime;
        // 如果偏移时间大于等于1，则开始攻击
        if (time >= fireOffsetTime)
        {
            Fire();
            // 重置偏移时间计数
            time = 0;
        }
        // 每次更新旋转头部
        head.Rotate(Vector3.up * headRotateSpeed * Time.deltaTime);
    }
    public override void Fire()
    {
        // 如果攻击位置和子弹预设体不为空，则开始攻击
        if (firePosition != null && bulletPrefab != null)
        {
            // 遍历攻击位置
            for (int i = 0; i < firePosition.Length; i++)
            {
                // 实例化子弹，并设置父对象
                Bullet bullet = Instantiate(bulletPrefab, firePosition[i].position, firePosition[i].rotation).GetComponent<Bullet>();
                bullet.SetGrandFather(this);
            }
        }
    }
    public override void Hurt(TankBase other)
    {
        
    }
}