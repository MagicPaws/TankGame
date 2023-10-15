using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : PanelBase<GamePanel>
{
    public CustomGUILabel labelScore;
    public CustomGUILabel labelTime;
    public CustomGUIButton buttonSetting;
    public CustomGUIButton buttonQuit;
    public CustomGUITexture textureHp;

    public CustomGUITexture textureWeaponIcon;
    public CustomGUILabel labelNowBulletNum;
    public int nowBulletNum;
    public CustomGUILabel LabelMaxBulletNum;
    public int maxBulletNum;
    [HideInInspector]
    public float nowReloadTime;
    public Image reloadCusorImage;
    public AudioSource reloadAudioSource;
    private float reloadTime = 0;

    public int hpWidth = 350;
    [HideInInspector]
    public float nowtime = 0;
    private int time;
    [HideInInspector]
    public int nowScore = 0;
    private void Start()
    {
        if (Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        labelTime.content.text = "0秒";
        buttonSetting.clickEvent += () =>
        {
            // 打开设置面板按钮
            SettingPanel.Instance.ShowMe();
        };
        buttonQuit.clickEvent += () =>
        {
            // 退出按钮
            QuitPanel.Instance.ShowMe();
        };

    }
    private void Update()
    {
        nowtime += Time.deltaTime;
        time = (int)nowtime;
        labelTime.content.text = "";
        if (time / 3600 > 0)
        {
            labelTime.content.text += time / 3600 + "时";
        }
        if (time % 3600 / 60 > 0 || labelTime.content.text != "")
        {
            labelTime.content.text += time % 3600 / 60 + "分";
        }
        labelTime.content.text += time % 60 + "秒";

        if (reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
            reloadCusorImage.transform.position = Input.mousePosition;
            reloadCusorImage.transform.GetChild(0).GetComponent<Image>().fillAmount = reloadTime / nowReloadTime;
        }
        else
        {
            reloadCusorImage.gameObject.SetActive(false);
        }
    }
    public void AddScore(int score)
    {
        nowScore += score;
        labelScore.content.text = nowScore.ToString();
    }
    public void UpdateHp(int maxHp, int nowHp)
    {
        if (nowHp <= 0)
            textureHp.pos.width = 0;
        else
            textureHp.pos.width = (float)nowHp / maxHp * hpWidth;
    }
    public void ChangeWeapon(Texture weaponIcon, int maxBulletNum, float reloadTime, AudioClip reloadAudioClip)
    {
        if (reloadCusorImage.gameObject.activeSelf)
        {
            reloadCusorImage.gameObject.SetActive(false);
        }
        textureWeaponIcon.content.image = weaponIcon;
        LabelMaxBulletNum.content.text = maxBulletNum.ToString();
        this.maxBulletNum = maxBulletNum;
        labelNowBulletNum.content.text = maxBulletNum.ToString();
        nowBulletNum = maxBulletNum;
        nowReloadTime = reloadTime;
        
        reloadAudioSource.clip = reloadAudioClip;
    }
    public void UseBullet()
    {
        nowBulletNum--;
        labelNowBulletNum.content.text = nowBulletNum.ToString();
        if (nowBulletNum == 0)
        {
            Reload();
        }
    }
    private void Reload()
    {
        if (reloadAudioSource != null && reloadAudioSource.clip != null)
        {
            EffectManager.Instance.SetEffect(reloadAudioSource);
            reloadAudioSource.Play();
        }
        reloadTime = nowReloadTime;

        reloadCusorImage.transform.position = Input.mousePosition;
        reloadCusorImage.gameObject.SetActive(true);
        Invoke("FinishiReload", nowReloadTime);
    }
    private void FinishiReload()
    {
        nowBulletNum = maxBulletNum;
    }
}
