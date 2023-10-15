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
    public E_PropertyRewar_Type type;
    public GameObject effect;
    public int changeValue = 2;
    public float rotateSpeed;
    private void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerTank player = other.GetComponent<PlayerTank>();
            if (effect != null)
            {
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
                    player.hp += changeValue;
                    if(player.hp > player.maxHp)
                    {
                        player.hp = player.maxHp;
                    }
                    GamePanel.Instance.UpdateHp(player.maxHp, player.hp);
                    break;
                case E_PropertyRewar_Type.MaxHp:
                    player.maxHp += changeValue;
                    player.hp += changeValue;
                    GamePanel.Instance.UpdateHp(player.maxHp, player.hp);
                    break;
                case E_PropertyRewar_Type.Score:
                    GamePanel.Instance.AddScore(changeValue);
                    break;
                case E_PropertyRewar_Type.Atk:
                    player.atk += changeValue;
                    break;
                case E_PropertyRewar_Type.Def:
                    player.def += changeValue;
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}