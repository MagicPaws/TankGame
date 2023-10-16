using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : PanelBase<LosePanel>
{
    // 声明按钮
    public CustomGUIButton buttonQuit;
    public CustomGUIButton buttonGoOn;
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
            // 加载游戏场景
            SceneManager.LoadScene("GameScene");
        };
        // 隐藏面板
        HideMe();
    }
    public override void ShowMe()
    {
        // 暂停时间
        Time.timeScale = 0;
        // 调用父类的显示方法
        base.ShowMe();
    }
}