using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TankBase grandFather;
    public GameObject endEffectPrefab;
    public GameObject startEffectPrefab;
    public float moveSpeed = 20f;
    public bool isAoe = false;
    public float aoeRidius = 1f;

    private void Start()
    {
        PlayerEffect(true);
        Destroy(this.gameObject, 10f);
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
    }
    public void SetGrandFather(TankBase grandFather)
    {
        this.grandFather = grandFather;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isAoe)
        {
            if (other.tag == "Ground" || other.tag == "DestroyableObj" && grandFather.tag == "Enemy")
            {
                PlayerEffect(false);
                Destroy(this.gameObject);
            }
            else if (other.tag == "DestroyableObj" && grandFather.tag == "Player")
            {
                PlayerEffect(false);
                other.GetComponent<DestoryableObject>().Death(transform);
                Destroy(this.gameObject);
            }
            else if (grandFather != null && other.tag == "Enemy" && grandFather.tag == "Player"||
                     grandFather != null && other.tag == "Player" && grandFather.tag == "Enemy")
            {
                TankBase otherTankBase = other.GetComponent<TankBase>();
                PlayerEffect(false);
                otherTankBase.Hurt(grandFather);
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (other.tag == "Ground" || 
                other.tag == "DestroyableObj" ||
                other.tag == "Enemy" && grandFather.tag == "Player" ||
                other.tag == "Player" && grandFather.tag == "Enemy")
            {
                PlayerEffect(false);
                Collider[] others = Physics.OverlapSphere(transform.position, aoeRidius);
                for (int i = 0; i < others.Length; i++)
                {
                    switch (others[i].tag)
                    {
                        case "DestroyableObj":
                            if (grandFather.tag == "Player")
                            {
                                others[i].GetComponent<DestoryableObject>().Death(transform);
                            }
                            break;
                        case "Enemy":
                            if (grandFather.tag == "Player")
                            {
                                others[i].GetComponent<TankBase>().Hurt(grandFather);
                            }
                            break;
                        case "Player":
                            if (grandFather.tag == "Enemy")
                            {
                                others[i].GetComponent<TankBase>().Hurt(grandFather);
                            }
                            break;
                    }
                }

                Destroy(this.gameObject);
            }
        }
    }
    private void PlayerEffect(bool start)
    {
        if (start && startEffectPrefab != null)
        {
            GameObject effect = Instantiate(startEffectPrefab, transform.position, transform.rotation);
            AudioSource effectAudio;
            if (effect.TryGetComponent<AudioSource>(out effectAudio))
            {
                EffectManager.Instance.SetEffect(effectAudio);
                effectAudio.Play();
            }
        }
        else if(!start && endEffectPrefab != null)
        {
            GameObject effect = Instantiate(endEffectPrefab, transform.position, transform.rotation);
            AudioSource effectAudio;
            if (effect.TryGetComponent<AudioSource>(out effectAudio))
            {
                EffectManager.Instance.SetEffect(effectAudio);
                effectAudio.Play();
            }
        }
    }
}
