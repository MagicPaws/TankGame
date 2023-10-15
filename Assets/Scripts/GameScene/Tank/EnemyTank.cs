using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : TankBase
{
    public float fireOffsetTime = 1f;
    public Transform[] firePosition;
    public GameObject bulletPrefab;
    public Transform[] movePositions;
    public Transform tragePosition;
    public Transform player;
    public float pursuitDistance = 10f;
    public float giveUpDistance = 20f;

    public Texture textureHpBg;
    public Texture textureHp;
    private Rect hpRect;

    private float hpShowTime = 0;
    private float time = 0;
    private void Start()
    {
        if (tragePosition == null && movePositions != null)
        {
            tragePosition = RandomMovePosition();
        }
        if (PlayerTank.Instance != null)
        {
            player = PlayerTank.Instance.transform;
        }
    }
    private void OnGUI()
    {
        if (hpShowTime > 0)
        {
            hpShowTime -= Time.deltaTime;

            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            hpRect.height = 400 / screenPoint.z;
            hpRect.width = 1500 / screenPoint.z;
            hpRect.x = screenPoint.x - hpRect.width / 2;
            hpRect.y = Screen.height - screenPoint.y - hpRect.height * 2.5f;
            GUI.DrawTexture(hpRect, textureHpBg);
            hpRect.width *= (float)hp / maxHp;
            GUI.DrawTexture(hpRect, textureHp);
        }
    }
    private void Update()
    {
        time += Time.deltaTime;
        // 在固定选定的位置自动移动
        if (tragePosition != null)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
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
