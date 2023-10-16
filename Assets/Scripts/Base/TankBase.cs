using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBase : MonoBehaviour
{
    // 攻击力
    public int atk;
    // 防御力
    public int def;
    // 最大血量
    public int maxHp;
    // 当前血量
    public int hp;

    // 头部
    public Transform head;

    // 移动速度
    public float moveSpeed = 10;
    // 旋转速度
    public float rotateSpeed = 10;
    // 头部旋转速度
    public float headRotateSpeed = 20;

    // 效果预览
    public GameObject effectPrefab;
    // 发射
    public abstract void Fire();
    // 受到伤害
    public virtual void Hurt(TankBase other)
    {
        int damage = other.atk - def;
        if (damage <= 0)
            hp -= 1;
        else if (damage >= maxHp)
            hp -= maxHp - 1;
        else
            hp -= damage;
        if (hp <= 0)
            Death();
    }
    // 死亡
    public virtual void Death()
    {
        if (effectPrefab != null)
        {
            GameObject effectObj = Instantiate(effectPrefab, transform.position, transform.rotation);
            AudioSource audioSource = effectObj.GetComponent<AudioSource>();
            EffectManager.Instance.SetEffect(audioSource);
            audioSource.Play();
        }
        Destroy(this.gameObject, 0.02f);
    }
}