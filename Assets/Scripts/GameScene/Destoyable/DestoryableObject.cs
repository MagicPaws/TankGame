using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryableObject : MonoBehaviour
{
    // 奖励对象
    public GameObject[] rewards;
    // 效果对象
    public GameObject effect;
    // 实例概率
    [Range(0,1)]
    public float InstantiateProbability = 0.5f;
    public void Death(Transform other)
    {
        Bullet bullet;
        // 获取子弹组件
        if (other.gameObject.TryGetComponent<Bullet>(out bullet))
        {
            // 判断是否是玩家
            if (bullet.grandFather.tag == "Player")
            {
                // 播放效果
                if (effect != null)
                {
                    AudioSource effectAudio;
                    // 获取效果音频组件
                    if (Instantiate(effect, transform.position, transform.rotation).TryGetComponent<AudioSource>(out effectAudio))
                    {
                        // 设置效果音频
                        EffectManager.Instance.SetEffect(effectAudio);
                        // 播放效果音频
                        effectAudio.Play();
                    }
                }
                // 判断是否随机实例化奖励
                if (Random.Range(0f, 1f) < InstantiateProbability)
                {
                    // 随机实例化奖励
                    Instantiate(rewards[Random.Range(0, rewards.Length)], transform.position, transform.rotation);
                }
                // 销毁对象
                Destroy(this.gameObject);
            }
        }

    }
}