using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    public float destoryTime = 0f;
    void Awake()
    {
        if (destoryTime == 0)
        {
            Destroy(this.gameObject, GetComponentInChildren<ParticleSystem>().main.duration);
        }
        else
        {
            Destroy(this.gameObject, destoryTime);
        }
    }
}
