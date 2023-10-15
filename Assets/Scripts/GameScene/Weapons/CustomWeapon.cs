using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform[] firePosition;
    public float fireOffsetTime = 1f;
    public int maxBulletNum = 30;
    public float reloadTime = 1f;
    public Texture WeaponIcon;
    public AudioClip reloadAudioClip;

    private float time;
    public TankBase father;
    public void SetFather(TankBase father)
    {
        this.father = father;
    }
    public void Update()
    {
        time += Time.deltaTime;
    }
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
