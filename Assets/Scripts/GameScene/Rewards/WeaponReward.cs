using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    // 旋转速度
    public float rotateSpeed = 30f;
    // 武器列表
    public GameObject[] weapons;
    // 效果
    public GameObject effect;
    void Update()
    {
        // 旋转
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        // 判断是否是玩家
        if (other.tag == "Player")
        {
            // 判断武器列表是否为空
            if (weapons != null)
            {
                // 获取当前武器索引
                int weaponIndex = GetWeaponIndex(other.GetComponent<PlayerTank>().nowWeaponIndex);
                // 更改当前武器
                other.GetComponent<PlayerTank>().ChangeWeapon(weapons[weaponIndex], weaponIndex);
                // 判断效果是否存在
                if (effect != null)
                {
                    // 获取效果音频
                    AudioSource effectAudio;
                    // 判断是否获取到音频
                    if (Instantiate(effect, transform.position, transform.rotation).TryGetComponent<AudioSource>(out effectAudio))
                    {
                        // 设置效果音频
                        EffectManager.Instance.SetEffect(effectAudio);
                        // 播放音频
                        effectAudio.Play();
                    }
                }
            }

            // 销毁
            Destroy(this.gameObject);
        }
    }
    private int GetWeaponIndex(int nowWeaponIndex)
    {
        // 随机获取一个索引
        int index = Random.Range(0, weapons.Length);
        // 判断索引是否和当前武器索引相同
        if (index == nowWeaponIndex)
        {
            // 递归调用
            return GetWeaponIndex(nowWeaponIndex);
        }

        return index;
    }
}