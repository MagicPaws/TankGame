using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : PanelBase<QuitPanel>
{
    // 声明按钮
    public CustomGUIButton buttonQuit;
    public CustomGUIButton buttonGoOn;
    public CustomGUIButton buttonClose;

    private void Start()
    {
        // 为按钮添加点击事件
        buttonQuit.clickEvent += () =>
        {
            // 加载开始场景
            SceneManager.LoadScene("BeginScene");
        };
        buttonGoOn.clickEvent += () =>
        {
            // 隐藏面板
            HideMe();
        };
        buttonClose.clickEvent += () =>
        {
            // 隐藏面板
            HideMe();
        };

        // 隐藏面板
        HideMe();
    }
    public override void ShowMe()
    {
        // 调用父类的ShowMe方法
        base.ShowMe();
        // 设置时间缩放为0
        Time.timeScale = 0;
    }
    public override void HideMe()
    {
        // 调用父类的HideMe方法
        base.HideMe();
        // 设置时间缩放为1
        Time.timeScale = 1;
    }
}