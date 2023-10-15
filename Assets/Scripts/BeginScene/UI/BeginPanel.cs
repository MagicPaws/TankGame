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
            // 开始按钮
            SceneManager.LoadScene("GameScene");
        };
        settingBtn.clickEvent += () =>
        {
            // 设置按钮
            SettingPanel.Instance.ShowMe();
            HideMe();
        };
        quitBtn.clickEvent += () =>
        {
            // 退出按钮
            Application.Quit();
        };
        levelBtn.clickEvent += () =>
        {
            // 排行榜按钮
            LevelPanel.Instance.ShowMe();
            HideMe();
        };
    }
}
