using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : PanelBase<LevelPanel>
{
    // 关闭按钮
    public CustomGUIButton closeBtn;
    // 名字标签列表
    private List<CustomGUILabel> names = new List<CustomGUILabel>();
    // 分数标签列表
    private List<CustomGUILabel> scores = new List<CustomGUILabel>();
    // 时间标签列表
    private List<CustomGUILabel> times = new List<CustomGUILabel>();

    private void Start()
    {
        // 遍历10个标签
        for (int i = 1; i <= 10; i++)
        {
            // 获取标签
            names.Add(transform.Find("Name/Label_Name" + i).GetComponent<CustomGUILabel>());
            scores.Add(transform.Find("Score/Label_Score" + i).GetComponent<CustomGUILabel>());
            times.Add(transform.Find("Time/Label_Time" + i).GetComponent<CustomGUILabel>());
        }
        // 添加点击事件
        closeBtn.clickEvent += () =>
        {
            // 调用BeginPanel的ShowMe方法
            BeginPanel.Instance.ShowMe();
            // 隐藏当前面板
            HideMe();

        };

        // 隐藏当前面板
        HideMe();
    }
    public override void ShowMe()
    {
        // 调用父类的ShowMe方法
        base.ShowMe();

        // 更新数据
        UpdateLevelData();
    }
    public void UpdateLevelData()
    {
        // 获取排行榜信息
        List<RankInfo> list = DataManager.Instance.leveData.rankInfoList;
        // 遍历排行榜信息
        for (int i = 0; i < list.Count; i++)
        {
            // 设置名字标签
            names[i].content.text = list[i].name;
            // 设置分数标签
            scores[i].content.text = list[i].score.ToString();
            // 获取时间
            int time = (int)list[i].time;
            // 设置时间标签
            times[i].content.text = "";
            // 如果时间大于3600秒，则设置小时
            if (time / 3600 > 0)
            {
                times[i].content.text += time / 3600 + "时";
            }
            // 如果时间大于60秒，则设置分钟
            if (time % 3600 / 60 > 0 || times[i].content.text != "")
            {
                times[i].content.text += time % 3600 / 60 + "分";
            }
            // 设置秒
            times[i].content.text += time % 60 + "秒";
        }
    }
}