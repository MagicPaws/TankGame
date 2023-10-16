using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEnemyTower : TankBase
{
    // 血条背景纹理
    public Texture textureHpBg;
    // 血条纹理
    public Texture textureHp;
    // 血条矩形
    private Rect hpRect;

    // 显示血条的时间
    private float hpShowTime = 0;

    private void Update()
    {
        // 旋转头部
        head.transform.Rotate(Vector3.up * headRotateSpeed * Time.deltaTime);
    }
    private void OnGUI()
    {
        // 如果显示血条的时间大于0
        if (hpShowTime > 0)
        {
            // 每次减少0.01秒
            hpShowTime -= Time.deltaTime;

            // 获取屏幕坐标
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            // 计算血条矩形的宽高
            hpRect.height = 400 / screenPoint.z;
            hpRect.width = 1500 / screenPoint.z;
            // 计算血条矩形的坐标
            hpRect.x = screenPoint.x - hpRect.width / 2;
            hpRect.y = Screen.height - screenPoint.y - hpRect.height * 2.5f;
            // 绘制血条背景
            GUI.DrawTexture(hpRect, textureHpBg);
            // 计算血条矩形的宽度
            hpRect.width *= (float)hp / maxHp;
            // 绘制血条
            GUI.DrawTexture(hpRect, textureHp);
        }
    }
    public override void Fire()
    {

    }
    public override void Hurt(TankBase other)
    {
        // 调用父类的Hurt方法
        base.Hurt(other);
        // 显示血条时间设置为2秒
        hpShowTime = 2;
    }
    public override void Death()
    {
        // 设置对象不可见
        gameObject.SetActive(false);
        // 如果效果预设体不为空
        if (effectPrefab != null)
        {
            // 实例化效果预设体
            GameObject effectObj = Instantiate(effectPrefab, transform.position, transform.rotation);
            // 获取AudioSource组件
            AudioSource audioSource = effectObj.GetComponent<AudioSource>();
            // 设置效果
            EffectManager.Instance.SetEffect(audioSource);
            // 播放音频
            audioSource.Play();
        }
        // 显示胜利面板
        ShowWinPanel();
    }
    private void ShowWinPanel()
    {
        // 显示胜利面板
        WinPanel.Instance.ShowMe();
    }
}