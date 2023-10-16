using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBase : MonoBehaviour
{
    public int atk;
    public int def;
    public int maxHp;
    public int hp;

    public Transform head;

    public float moveSpeed = 10;
    public float rotateSpeed = 10;
    public float headRotateSpeed = 20;

    public GameObject effectPrefab;
    public abstract void Fire();
    public virtual void Hurt(TankBase other)
    {
        int damage = other.atk - def;
        if (damage <= 0)
            hp -= 1;
        else if (damage >= other.maxHp)
            hp -= maxHp - 1;
        else
            hp -= damage;
        if (hp <= 0)
            Death();
    }
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
