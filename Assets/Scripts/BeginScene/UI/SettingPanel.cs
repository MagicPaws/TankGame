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
            // ���ֿ���
            DataManager.Instance.SetMusicOpenOrOff(value);
        };
        effectToggle.changeValueEvent += (value) =>
        {
            // ��Ч����
            DataManager.Instance.SetEffectOpenOrOff(value);
        };
        musicSlider.changeValueEvent += (value) =>
        {
            // ����������С�ı�
            DataManager.Instance.SetMusicValue(value);
        };
        effectSlider.changeValueEvent += (value) =>
        {
            // ��Ч������С�ı�
            DataManager.Instance.SetEffectValue(value);
        };
        closeBtn.clickEvent += () =>
        {
            // �ر��������
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
        effectToggle.isSelect = DataManager.Instance.musicData.effectIsOpen;
        effectSlider.nowValue = DataManager.Instance.musicData.effectValue;
    }
}
