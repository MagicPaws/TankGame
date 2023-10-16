using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    // 销毁时间
    public float destoryTime = 0f;
    void Awake()
    {
        // 如果销毁时间为0
        if (destoryTime == 0)
        {
            // 销毁当前游戏对象，并等待粒子系统的持续时间后销毁
            Destroy(this.gameObject, GetComponentInChildren<ParticleSystem>().main.duration);
        }
        else
        {
            // 否则销毁当前游戏对象，并等待指定的销毁时间后销毁
            Destroy(this.gameObject, destoryTime);
        }
    }
}