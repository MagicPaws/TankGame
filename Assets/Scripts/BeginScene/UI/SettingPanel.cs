using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : PanelBase<SettingPanel>
{
    public CustomGUIToggle musicToggle;
    public CustomGUIToggle effectToggle;

    public CustomGUISlider musicSlider;
    public CustomGUISlider effectSlider;

    public CustomGUIButton closeBtn;

    private void Start()
    {
        musicToggle.changeValueEvent += (value) =>
        {
            // 音乐开关
            DataManager.Instance.SetMusicOpenOrOff(value);
        };
        effectToggle.changeValueEvent += (value) =>
        {
            // 音效开关
            DataManager.Instance.SetEffectOpenOrOff(value);
        };
        musicSlider.changeValueEvent += (value) =>
        {
            // 音乐声音大小改变
            DataManager.Instance.SetMusicValue(value);
        };
        effectSlider.changeValueEvent += (value) =>
        {
            // 音效声音大小改变
            DataManager.Instance.SetEffectValue(value);
        };
        closeBtn.clickEvent += () =>
        {
            // 关闭设置面板
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }
            HideMe();
        };

        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        UpdateMusicData();

        Time.timeScale = 0;
    }
    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
    public void UpdateMusicData()
    {
        musicToggle.isSelect = DataManager.Instance.musicData.musicIsOpen;
        musicSlider.nowValue = DataManager.Instance.musicData.musicValue;
        effectToggle.isSelect = DataManager.Instance.musicData.soundIsOpen;
        effectSlider.nowValue = DataManager.Instance.musicData.soundValue;
    }
}
