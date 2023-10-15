using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    public float rotateSpeed = 30f;
    public GameObject[] weapons;
    public GameObject effect;
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (weapons != null)
            {
                int weaponIndex = GetWeaponIndex(other.GetComponent<PlayerTank>().nowWeaponIndex);
                other.GetComponent<PlayerTank>().ChangeWeapon(weapons[weaponIndex], weaponIndex);
                if (effect != null)
                {
                    AudioSource effectAudio;
                    if (Instantiate(effect, transform.position, transform.rotation).TryGetComponent<AudioSource>(out effectAudio))
                    {
                        EffectManager.Instance.SetEffect(effectAudio);
                        effectAudio.Play();
                    }
                }
            }

            Destroy(this.gameObject);
        }
    }
    private int GetWeaponIndex(int nowWeaponIndex)
    {
        int index = Random.Range(0, weapons.Length);
        if (index == nowWeaponIndex)
        {
            return GetWeaponIndex(nowWeaponIndex);
        }

        return index;
    }
}
