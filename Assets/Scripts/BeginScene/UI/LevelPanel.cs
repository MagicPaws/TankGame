using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : PanelBase<LevelPanel>
{
    public CustomGUIButton closeBtn;
    private List<CustomGUILabel> names = new List<CustomGUILabel>();
    private List<CustomGUILabel> scores = new List<CustomGUILabel>();
    private List<CustomGUILabel> times = new List<CustomGUILabel>();

    private void Start()
    {
        for (int i = 1; i <= 10; i++)
        {
            names.Add(transform.Find("Name/Label_Name" + i).GetComponent<CustomGUILabel>());
            scores.Add(transform.Find("Score/Label_Score" + i).GetComponent<CustomGUILabel>());
            times.Add(transform.Find("Time/Label_Time" + i).GetComponent<CustomGUILabel>());
        }
        closeBtn.clickEvent += () =>
        {
            BeginPanel.Instance.ShowMe();
            HideMe();

        };

        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();

        UpdateLevelData();
    }
    public void UpdateLevelData()
    {
        List<RankInfo> list = DataManager.Instance.leveData.rankInfoList;
        for (int i = 0; i < list.Count; i++)
        {
            names[i].content.text = list[i].name;
            scores[i].content.text = list[i].score.ToString();
            int time = (int)list[i].time;
            times[i].content.text = "";
            if (time / 3600 > 0)
            {
                times[i].content.text += time / 3600 + " ±";
            }
            if (time % 3600 / 60 > 0 || times[i].content.text != "")
            {
                times[i].content.text += time % 3600 / 60 + "∑÷";
            }
            times[i].content.text += time % 60 + "√Î";
        }
    }
}
