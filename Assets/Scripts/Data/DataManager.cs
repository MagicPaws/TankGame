using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // 定义一个静态的DataManager实例
    private static DataManager instance = new DataManager();
    // 获取DataManager实例
    public static DataManager Instance => instance;
    // 构造函数
    private DataManager()
    {
        // 从本地存储中加载音乐数据
        musicData = PlayerPrefsDataMgr.Instance.LoadData("Music", typeof(MusicData)) as MusicData;

        // 如果不是第一次打开游戏
        if (!musicData.notFirst)
        {
            // 将notFirst设置为true
            musicData.notFirst = true;
            // 将musicIsOpen设置为true
            musicData.musicIsOpen = true;
            // 将soundIsOpen设置为true
            musicData.soundIsOpen = true;
            // 将musicValue设置为1
            musicData.musicValue = 1;
            // 将soundValue设置为1
            musicData.soundValue = 1;
        }

        // 从本地存储中加载关卡数据
        leveData = PlayerPrefsDataMgr.Instance.LoadData("Level", typeof(LevelData)) as LevelData;
    }

    // 定义音乐数据
    public MusicData musicData;
    // 定义关卡数据
    public LevelData leveData;

    // 设置音乐播放状态
    public void SetMusicOpenOrOff(bool OpenOrOff)
    {
        // 设置musicIsOpen的值
        musicData.musicIsOpen = OpenOrOff;
        // 改变音乐播放状态
        MusicManager.Instance.ChangeMusicPlay(OpenOrOff);
        // 保存数据
        PlayerPrefsDataMgr.Instance.SaveData("Music", musicData);
    }
    // 设置音乐音量
    public void SetMusicValue(float value)
    {
        // 设置musicValue的值
        musicData.musicValue = value;
        // 改变音乐音量
        MusicManager.Instance.ChangeMusicValue(value);
        // 保存数据
        PlayerPrefsDataMgr.Instance.SaveData("Music", musicData);
    }
    // 设置音效播放状态
    public void SetEffectOpenOrOff(bool OpenOrOff)
    {
        // 设置soundIsOpen的值
        musicData.soundIsOpen = OpenOrOff;
        // 保存数据
        PlayerPrefsDataMgr.Instance.SaveData("Music", musicData);
    }
    // 设置音效音量
    public void SetEffectValue(float value)
    {
        // 设置soundValue的值
        musicData.soundValue = value;
        // 保存数据
        PlayerPrefsDataMgr.Instance.SaveData("Music", musicData);
    }
    // 添加排行榜信息
    public void AddRankInfo(string name, int score, float time)
    {
        // 将排行榜信息添加到列表中
        leveData.rankInfoList.Add(new RankInfo(name, score, time));
        // 根据时间对排行榜信息进行排序
        leveData.rankInfoList.Sort((a, b) =>
        {
            if (a.time != b.time)
            {
                return a.time > b.time ? 1 : -1;
            }
            else
            {
                return a.score < b.score ? 1 : -1;
            }
        });
        // 移除排行榜中多余的信息
        for (int i = leveData.rankInfoList.Count - 1; i >= 10; i--)
        {
            leveData.rankInfoList.RemoveAt(i);
        }
        // 保存数据
        PlayerPrefsDataMgr.Instance.SaveData("Level", leveData);
    }
}