using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : PanelBase<WinPanel>
{
    // 输入框
    public CustomGUIInput inputName;
    // 按钮
    public CustomGUIButton buttonEnter;
    // 是否改变名字
    private bool isChangeName = false;
    private void Start()
    {
        // 输入框文本改变事件
        inputName.changeText += (name) =>
        {
            isChangeName = true;
        };
        // 按钮点击事件
        buttonEnter.clickEvent += () =>
        {
            if (isChangeName)
            {
                // 添加排行榜信息
                DataManager.Instance.AddRankInfo(inputName.nowText, GamePanel.Instance.nowScore, GamePanel.Instance.nowtime);
                // 切换场景
                SceneManager.LoadScene("BeginScene");
            }
        };
        // 隐藏面板
        HideMe();
    }
    // 显示面板
    public override void ShowMe()
    {
        // 暂停时间
        Time.timeScale = 0;
        // 调用父类的显示方法
        base.ShowMe();
    }
}