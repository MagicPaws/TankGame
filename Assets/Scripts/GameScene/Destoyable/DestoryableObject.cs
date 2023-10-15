using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryableObject : MonoBehaviour
{
    public GameObject[] rewards;
    public GameObject effect;
    [Range(0,1)]
    public float InstantiateProbability = 0.5f;
    public void Death(Transform other)
    {
        Bullet bullet;
        if (other.gameObject.TryGetComponent<Bullet>(out bullet))
        {
            if (bullet.grandFather.tag == "Player")
            {
                if (effect != null)
                {
                    AudioSource effectAudio;
                    if (Instantiate(effect, transform.position, transform.rotation).TryGetComponent<AudioSource>(out effectAudio))
                    {
                        EffectManager.Instance.SetEffect(effectAudio);
                        effectAudio.Play();
                    }
                }
                if (Random.Range(0f, 1f) < InstantiateProbability)
                {
                    Instantiate(rewards[Random.Range(0, rewards.Length)], transform.position, transform.rotation);
                }
                Destroy(this.gameObject);
            }
        }

    }
}
