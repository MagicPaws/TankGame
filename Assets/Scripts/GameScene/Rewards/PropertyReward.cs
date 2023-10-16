using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PropertyRewar_Type
{
    Hp,
    MaxHp,
    Score,
    Atk,
    Def,
}

public class PropertyReward : MonoBehaviour
{
    // 属性奖励类型
    public E_PropertyRewar_Type type;
    // 效果
    public GameObject effect;
    // 改变值
    public int changeValue = 2;
    // 旋转速度
    public float rotateSpeed;
    private void Update()
    {
        // 旋转
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 玩家触发
        if (other.tag == "Player")
        {
            PlayerTank player = other.GetComponent<PlayerTank>();
            if (effect != null)
            {
                // 播放效果音效
                AudioSource effectAudio;
                if (Instantiate(effect, transform.position, transform.rotation).TryGetComponent<AudioSource>(out effectAudio))
                {
                    EffectManager.Instance.SetEffect(effectAudio);
                    effectAudio.Play();
                }
            }
            switch (type)
            {
                case E_PropertyRewar_Type.Hp:
                    // 增加血量
                    player.hp += changeValue;
                    // 血量不能超过最大血量
                    if(player.hp > player.maxHp)
                    {
                        player.hp = player.maxHp;
                    }
                    // 更新血量
                    GamePanel.Instance.UpdateHp(player.maxHp, player.hp);
                    break;
                case E_PropertyRewar_Type.MaxHp:
                    // 增加最大血量
                    player.maxHp += changeValue;
                    // 增加血量
                    player.hp += changeValue;
                    // 更新血量
                    GamePanel.Instance.UpdateHp(player.maxHp, player.hp);
                    break;
                case E_PropertyRewar_Type.Score:
                    // 增加分数
                    GamePanel.Instance.AddScore(changeValue);
                    break;
                case E_PropertyRewar_Type.Atk:
                    // 增加攻击力
                    player.atk += changeValue;
                    break;
                case E_PropertyRewar_Type.Def:
                    // 增加防御力
                    player.def += changeValue;
                    break;
            }

            // 销毁对象
            Destroy(this.gameObject);
        }
    }
}