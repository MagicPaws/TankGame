using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWeapon : MonoBehaviour
{
    // 子弹预设体
    public GameObject bulletPrefab;
    // 发射位置
    public Transform[] firePosition;
    // 发射偏移时间
    public float fireOffsetTime = 1f;
    // 最大子弹数
    public int maxBulletNum = 30;
    // 加载时间
    public float reloadTime = 1f;
    // 武器图标
    public Texture WeaponIcon;
    // 加载音频
    public AudioClip reloadAudioClip;

    private float time;
    // 父类
    public TankBase father;
    // 设置父类
    public void SetFather(TankBase father)
    {
        this.father = father;
    }
    // 更新
    public void Update()
    {
        time += Time.deltaTime;
    }
    // 发射
    public void Fire()
    {
        if (bulletPrefab != null && father != null && time >= fireOffsetTime && GamePanel.Instance.nowBulletNum > 0)
        {
            time = 0;
            GamePanel.Instance.UseBullet();
            for (int i = 0; i < firePosition.Length; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePosition[i].position, firePosition[i].rotation);
                bullet.GetComponent<Bullet>().SetGrandFather(father);
            }
        }
    }
}