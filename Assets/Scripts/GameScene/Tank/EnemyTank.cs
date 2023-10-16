using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : TankBase
{
    // 攻击偏移时间
    public float fireOffsetTime = 1f;
    // 攻击位置
    public Transform[] firePosition;
    // 子弹预设体
    public GameObject bulletPrefab;
    // 移动位置
    public Transform[] movePositions;
    // 攻击位置
    public Transform tragePosition;
    // 玩家位置
    public Transform player;
    //  pursuitDistance：接近距离
    public float pursuitDistance = 10f;
    //  giveUpDistance：放弃距离
    public float giveUpDistance = 20f;

    // 血条背景
    public Texture textureHpBg;
    // 血条
    public Texture textureHp;
    private Rect hpRect;

    // 显示血条时间
    private float hpShowTime = 0;
    // 时间
    private float time = 0;
    private void Start()
    {
        // 如果没有攻击位置，则随机选择一个位置
        if (tragePosition == null && movePositions != null)
        {
            tragePosition = RandomMovePosition();
        }
        // 获取玩家位置
        if (PlayerTank.Instance != null)
        {
            player = PlayerTank.Instance.transform;
        }
    }
    private void OnGUI()
    {
        // 如果显示血条时间大于0，则显示血条
        if (hpShowTime > 0)
        {
            hpShowTime -= Time.deltaTime;

            // 获取屏幕坐标
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            // 计算血条位置
            hpRect.height = 400 / screenPoint.z;
            hpRect.width = 1500 / screenPoint.z;
            hpRect.x = screenPoint.x - hpRect.width / 2;
            hpRect.y = Screen.height - screenPoint.y - hpRect.height * 2.5f;
            // 绘制血条背景
            GUI.DrawTexture(hpRect, textureHpBg);
            // 绘制血条
            hpRect.width *= (float)hp / maxHp;
            GUI.DrawTexture(hpRect, textureHp);
        }
    }
    private void Update()
    {
        // 增加时间
        time += Time.deltaTime;
        // 在固定选定的位置自动移动
        if (tragePosition != null)
        {
            // 移动
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            // 旋转
            Quaternion qq = Quaternion.LookRotation(tragePosition.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qq, Time.deltaTime * rotateSpeed);
            qq = Quaternion.LookRotation(tragePosition.position - head.transform.position);
            head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, qq, Time.deltaTime * headRotateSpeed);
            // 到达位置附近自动切换地点
            if (Vector3.Distance(transform.position, tragePosition.position) < 0.05f)
            {
                tragePosition = RandomMovePosition();
            }
        }
        // 当玩家靠近后切换移动模式
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) < pursuitDistance)
            {
                tragePosition = player;
                Quaternion qq = Quaternion.LookRotation(player.position - head.transform.position);
                head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, qq, Time.deltaTime * headRotateSpeed);
                if (time >= fireOffsetTime)
                {
                    Fire();
                    time = 0;
                }
            }
            else if (Vector3.Distance(transform.position, player.position) > giveUpDistance && tragePosition == player)
            {
                tragePosition = RandomMovePosition();
            }
        }
    }
    private Transform RandomMovePosition()
    {
        if (movePositions.Length > 0)
        {
            return movePositions[Random.Range(0, movePositions.Length)];
        }
        return null;
    }
    public override void Fire()
    {
        if (firePosition != null && bulletPrefab != null)
        {
            for (int i = 0; i < firePosition.Length; i++)
            {
                Instantiate(bulletPrefab, firePosition[i].position, firePosition[i].rotation).GetComponent<Bullet>().SetGrandFather(this);
            }
        }
    }
    public override void Hurt(TankBase other)
    {
        base.Hurt(other);
        // 显示血条
        hpShowTime = 2;
    }
    public override void Death()
    {
        GamePanel.Instance.AddScore(50);
        base.Death();
    }
}