using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankInfo
{
    // 玩家名字
    public string name;
    // 玩家分数
    public int score;
    // 玩家用时
    public float time;
    public RankInfo()
    {
        //保留无参构造函数，用于实例化时内部调用使用无参构造实例化
    }
    public RankInfo(string name, int score, float time)
    {
        this.name = name;
        this.score = score;
        this.time = time;
    }
}

public class LevelData
{
    // 排行榜信息列表
    public List<RankInfo> rankInfoList;
}