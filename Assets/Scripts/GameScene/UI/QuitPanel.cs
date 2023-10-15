 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : PanelBase<QuitPanel>
{
    public CustomGUIButton buttonQuit;
    public CustomGUIButton buttonGoOn;
    public CustomGUIButton buttonClose;

    private void Start()
    {
        buttonQuit.clickEvent += () =>
        {
            SceneManager.LoadScene("BeginScene");
        };
        buttonGoOn.clickEvent += () =>
        {
            HideMe();
        };
        buttonClose.clickEvent += () =>
        {
            HideMe();
        };

        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0;
    }
    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
}
