using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    private static EffectManager instance = new EffectManager();
    public static EffectManager Instance => instance;
    private EffectManager()
    {

    }

    public void SetEffect(AudioSource effect)
    {
        effect.mute = !DataManager.Instance.musicData.effectIsOpen;
        effect.volume = DataManager.Instance.musicData.effectValue;
    }
}
