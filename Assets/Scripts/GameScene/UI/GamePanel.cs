using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : PanelBase<GamePanel>
{
    // 分数标签
    public CustomGUILabel labelScore;
    // 时间标签
    public CustomGUILabel labelTime;
    // 设置按钮
    public CustomGUIButton buttonSetting;
    // 退出按钮
    public CustomGUIButton buttonQuit;
    // 血量条
    public CustomGUITexture textureHp;

    // 武器图标
    public CustomGUITexture textureWeaponIcon;
    // 当前子弹数量标签
    public CustomGUILabel labelNowBulletNum;
    // 当前子弹数量
    public int nowBulletNum;
    // 最大子弹数量标签
    public CustomGUILabel LabelMaxBulletNum;
    // 最大子弹数量
    public int maxBulletNum;
    // 当前加载时间
    [HideInInspector]
    public float nowReloadTime;
    // 加载光标
    public Image reloadCusorImage;
    // 加载音效
    public AudioSource reloadAudioSource;
    private float reloadTime = 0;

    // 血量条宽度
    public int hpWidth = 350;
    // 当前时间
    [HideInInspector]
    public float nowtime = 0;
    // 总时间
    private int time;
    // 当前分数
    [HideInInspector]
    public int nowScore = 0;
    private void Start()
    {
        // 设置鼠标状态为 confined
        if (Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        // 设置时间标签内容
        labelTime.content.text = "0秒";
        // 设置设置按钮点击事件
        buttonSetting.clickEvent += () =>
        {
            // 打开设置面板按钮
            SettingPanel.Instance.ShowMe();
        };
        // 设置退出按钮点击事件
        buttonQuit.clickEvent += () =>
        {
            // 退出按钮
            QuitPanel.Instance.ShowMe();
        };

    }
    private void Update()
    {
        // 更新当前时间
        nowtime += Time.deltaTime;
        time = (int)nowtime;
        // 设置时间标签内容
        labelTime.content.text = "";
        // 判断总时间是否大于 3600 秒
        if (time / 3600 > 0)
        {
            // 设置时间标签内容
            labelTime.content.text += time / 3600 + "时";
        }
        // 判断总时间是否大于 60 秒
        if (time % 3600 / 60 > 0 || labelTime.content.text != "")
        {
            // 设置时间标签内容
            labelTime.content.text += time % 3600 / 60 + "分";
        }
        // 设置时间标签内容
        labelTime.content.text += time % 60 + "秒";

        // 判断加载时间是否大于 0
        if (reloadTime > 0)
        {
            // 减去加载时间
            reloadTime -= Time.deltaTime;
            // 设置加载光标位置
            reloadCusorImage.transform.position = Input.mousePosition;
            // 设置加载光标填充度
            reloadCusorImage.transform.GetChild(0).GetComponent<Image>().fillAmount = reloadTime / nowReloadTime;
        }
        else
        {
            // 隐藏加载光标
            reloadCusorImage.gameObject.SetActive(false);
        }
    }
    public void AddScore(int score)
    {
        // 添加分数
        nowScore += score;
        // 设置分数标签内容
        labelScore.content.text = nowScore.ToString();
    }
    public void UpdateHp(int maxHp, int nowHp)
    {
        // 判断当前血量是否小于等于 0
        if (nowHp <= 0)
            // 设置血量条宽度
            textureHp.pos.width = 0;
        else
            // 设置血量条宽度
            textureHp.pos.width = (float)nowHp / maxHp * hpWidth;
    }
    public void ChangeWeapon(Texture weaponIcon, int maxBulletNum, float reloadTime, AudioClip reloadAudioClip)
    {
        // 判断加载光标是否激活
        if (reloadCusorImage.gameObject.activeSelf)
        {
            // 隐藏加载光标
            reloadCusorImage.gameObject.SetActive(false);
        }
        // 设置武器图标
        textureWeaponIcon.content.image = weaponIcon;
        // 设置最大子弹数量标签内容
        LabelMaxBulletNum.content.text = maxBulletNum.ToString();
        // 设置最大子弹数量
        this.maxBulletNum = maxBulletNum;
        // 设置当前子弹数量标签内容
        labelNowBulletNum.content.text = maxBulletNum.ToString();
        // 设置当前子弹数量
        nowBulletNum = maxBulletNum;
        // 设置加载时间
        nowReloadTime = reloadTime;
        
        // 设置加载音效
        reloadAudioSource.clip = reloadAudioClip;
    }
    public void UseBullet()
    {
        // 使用子弹
        nowBulletNum--;
        // 设置当前子弹数量标签内容
        labelNowBulletNum.content.text = nowBulletNum.ToString();
        // 判断当前子弹数量是否小于等于 0
        if (nowBulletNum == 0)
        {
            // 加载子弹
            Reload();
        }
    }
    private void Reload()
    {
        // 加载音效
        if (reloadAudioSource != null && reloadAudioSource.clip != null)
        {
            // 设置音效
            EffectManager.Instance.SetEffect(reloadAudioSource);
            // 播放音效
            reloadAudioSource.Play();
        }
        // 设置加载时间
        reloadTime = nowReloadTime;

        // 设置加载光标位置
        reloadCusorImage.transform.position = Input.mousePosition;
        // 激活加载光标
        reloadCusorImage.gameObject.SetActive(true);
        // 延迟执行
        Invoke("FinishiReload", nowReloadTime);
    }
    private void FinishiReload()
    {
        // 设置当前子弹数量
        nowBulletNum = maxBulletNum;
    }
}