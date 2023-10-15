using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : PanelBase<LosePanel>
{
    public CustomGUIButton buttonQuit;
    public CustomGUIButton buttonGoOn;
    private void Start()
    {
        buttonQuit.clickEvent += () =>
        {
            SceneManager.LoadScene("BeginScene");
        };
        buttonGoOn.clickEvent += () =>
        {
            SceneManager.LoadScene("GameScene");
        };
        HideMe();
    }
    public override void ShowMe()
    {
        Time.timeScale = 0;
        base.ShowMe();
    }
}
