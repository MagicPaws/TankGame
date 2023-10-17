using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelectPanel : PanelBase<ModeSelectPanel>
{
   // 创建一个按钮，用于返回
    public CustomGUIButton btnBack;

    // 创建一个按钮，用于选择模式1
    public CustomGUIButton btnMode1;

    // 创建一个按钮，用于选择模式2
    public CustomGUIButton btnMode2;
    void Start()
    {
        btnBack.clickEvent += (() => {
            // 显示开始面板
            BeginPanel.Instance.ShowMe();
            this.HideMe();
        });
        btnMode1.clickEvent += (() => {
            // 显示闯关模式
            SceneManager.LoadScene("GameScene");
        });
        btnMode2.clickEvent += (() => {
            // 显示无尽模式
            SceneManager.LoadScene("EndlessScene");
        });
        HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
