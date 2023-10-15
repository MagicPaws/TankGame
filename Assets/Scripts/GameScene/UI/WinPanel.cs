using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : PanelBase<WinPanel>
{
    public CustomGUIInput inputName;
    public CustomGUIButton buttonEnter;
    private bool isChangeName = false;
    private void Start()
    {
        inputName.changeText += (name) =>
        {
            isChangeName = true;
        };
        buttonEnter.clickEvent += () =>
        {
            if (isChangeName)
            {
                DataManager.Instance.AddRankInfo(inputName.nowText, GamePanel.Instance.nowScore, GamePanel.Instance.nowtime);
                SceneManager.LoadScene("BeginScene");
            }
        };
        HideMe();
    }
    public override void ShowMe()
    {
        Time.timeScale = 0;
        base.ShowMe();
    }
}
