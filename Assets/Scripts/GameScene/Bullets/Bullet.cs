using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 父坦克
    public TankBase grandFather;
    // 结束效果预设
    public GameObject endEffectPrefab;
    // 开始效果预设
    public GameObject startEffectPrefab;
    // 移动速度
    public float moveSpeed = 20f;
    // 是否是AOE
    public bool isAoe = false;
    // AOE半径
    public float aoeRidius = 1f;

    private void Start()
    {
        // 开始时播放开始效果
        PlayerEffect(true);
        // 销毁自身，10秒后销毁
        Destroy(this.gameObject, 10f);
    }
    void FixedUpdate()
    {
        // 按照移动速度和时间差来更新位置
        transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
    }
    public void SetGrandFather(TankBase grandFather)
    {
        // 设置父坦克
        this.grandFather = grandFather;
    }
    private void OnTriggerEnter(Collider other)
    {
        // 如果不是AOE
        if (!isAoe)
        {
            // 如果碰撞的是地面或者可破坏物，并且是敌人
            if (other.tag == "Ground" || other.tag == "DestroyableObj" && grandFather.tag == "Enemy")
            {
                // 播放结束效果
                PlayerEffect(false);
                // 销毁自身
                Destroy(this.gameObject);
            }
            // 如果碰撞的是可破坏物，并且是玩家
            else if (other.tag == "DestroyableObj" && grandFather.tag == "Player")
            {
                // 播放结束效果
                PlayerEffect(false);
                // 销毁可破坏物
                other.GetComponent<DestoryableObject>().Death(transform);
                // 销毁自身
                Destroy(this.gameObject);
            }
            // 如果碰撞的是敌人，并且是玩家或者敌人
            else if (grandFather != null && other.tag == "Enemy" && grandFather.tag == "Player"||
                     grandFather != null && other.tag == "Player" && grandFather.tag == "Enemy")
            {
                // 获取碰撞的坦克
                TankBase otherTankBase = other.GetComponent<TankBase>();
                // 播放结束效果
                PlayerEffect(false);
                // 伤害玩家
                otherTankBase.Hurt(grandFather);
                // 销毁自身
                Destroy(this.gameObject);
            }
        }
        // 如果是AOE
        else
        {
            // 如果碰撞的是地面或者可破坏物，可敌人，玩家或者敌人
            if (other.tag == "Ground" || 
                other.tag == "DestroyableObj" ||
                other.tag == "Enemy" && grandFather.tag == "Player" ||
                other.tag == "Player" && grandFather.tag == "Enemy")
            {
                // 播放结束效果
                PlayerEffect(false);
                // 获取碰撞的碰撞体
                Collider[] others = Physics.OverlapSphere(transform.position, aoeRidius);
                // 遍历碰撞体
                for (int i = 0; i < others.Length; i++)
                {
                    // 根据碰撞体的标签来执行不同的操作
                    switch (others[i].tag)
                    {
                        case "DestroyableObj":
                            // 如果碰撞的是可破坏物，并且是玩家
                            if (grandFather.tag == "Player")
                            {
                                // 销毁可破坏物
                                others[i].GetComponent<DestoryableObject>().Death(transform);
                            }
                            break;
                        case "Enemy":
                            // 如果碰撞的是敌人，并且是玩家
                            if (grandFather.tag == "Player")
                            {
                                // 伤害玩家
                                others[i].GetComponent<TankBase>().Hurt(grandFather);
                            }
                            break;
                        case "Player":
                            // 如果碰撞的是玩家，并且是敌人
                            if (grandFather.tag == "Enemy")
                            {
                                // 伤害敌人
                                others[i].GetComponent<TankBase>().Hurt(grandFather);
                            }
                            break;
                    }
                }

                // 销毁自身
                Destroy(this.gameObject);
            }
        }
    }
    private void PlayerEffect(bool start)
    {
        // 如果开始，并且开始效果预设不为空
        if (start && startEffectPrefab != null)
        {
            // 实例化开始效果预设，并设置位置和旋转
            GameObject effect = Instantiate(startEffectPrefab, transform.position, transform.rotation);
            // 获取音频源
            AudioSource effectAudio;
            // 如果获取到音频源，则设置效果管理器
            if (effect.TryGetComponent<AudioSource>(out effectAudio))
            {
                EffectManager.Instance.SetEffect(effectAudio);
                // 播放音频
                effectAudio.Play();
            }
        }
        // 如果不是开始，并且结束效果预设不为空
        else if(!start && endEffectPrefab != null)
        {
            // 实例化结束效果预设，并设置位置和旋转
            GameObject effect = Instantiate(endEffectPrefab, transform.position, transform.rotation);
            // 获取音频源
            AudioSource effectAudio;
            // 如果获取到音频源，则设置效果管理器
            if (effect.TryGetComponent<AudioSource>(out effectAudio))
            {
                EffectManager.Instance.SetEffect(effectAudio);
                // 播放音频
                effectAudio.Play();
            }
        }
    }
}