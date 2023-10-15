using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginPanel : PanelBase<BeginPanel>
{
    public CustomGUIButton startBtn;
    public CustomGUIButton settingBtn;
    public CustomGUIButton quitBtn;
    public CustomGUIButton levelBtn;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        startBtn.clickEvent += () =>
        {
            // ��ʼ��ť
            SceneManager.LoadScene("GameScene");
        };
        settingBtn.clickEvent += () =>
        {
            // ���ð�ť
            SettingPanel.Instance.ShowMe();
            HideMe();
        };
        quitBtn.clickEvent += () =>
        {
            // �˳���ť
            Application.Quit();
        };
        levelBtn.clickEvent += () =>
        {
            // ���а�ť
            LevelPanel.Instance.ShowMe();
            HideMe();
        };
    }
}
