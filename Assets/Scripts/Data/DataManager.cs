using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager instance = new DataManager();
    public static DataManager Instance => instance;
    private DataManager()
    {
        musicData = PlayerPrefsDataMgr.Instance.LoadData("Music", typeof(MusicData)) as MusicData;

        if (!musicData.notFirst)
        {
            musicData.notFirst = true;
            musicData.musicIsOpen = true;
            musicData.effectIsOpen = true;
            musicData.musicValue = 1;
            musicData.effectValue = 1;
        }

        leveData = PlayerPrefsDataMgr.Instance.LoadData("Level", typeof(LevelData)) as LevelData;
    }

    public MusicData musicData;
    public LevelData leveData;

    public void SetMusicOpenOrOff(bool OpenOrOff)
    {
        musicData.musicIsOpen = OpenOrOff;
        MusicManager.Instance.ChangeMusicPlay(OpenOrOff);
        PlayerPrefsDataMgr.Instance.SaveData("Music", musicData);
    }
    public void SetMusicValue(float value)
    {
        musicData.musicValue = value;
        MusicManager.Instance.ChangeMusicValue(value);
        PlayerPrefsDataMgr.Instance.SaveData("Music", musicData);
    }
    public void SetEffectOpenOrOff(bool OpenOrOff)
    {
        musicData.effectIsOpen = OpenOrOff;
        PlayerPrefsDataMgr.Instance.SaveData("Music", musicData);
    }
    public void SetEffectValue(float value)
    {
        musicData.effectValue = value;
        PlayerPrefsDataMgr.Instance.SaveData("Music", musicData);
    }
    public void AddRankInfo(string name, int score, float time)
    {
        leveData.rankInfoList.Add(new RankInfo(name, score, time));
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
        for (int i = leveData.rankInfoList.Count - 1; i >= 10; i--)
        {
            leveData.rankInfoList.RemoveAt(i);
        }
        PlayerPrefsDataMgr.Instance.SaveData("Level", leveData);
    }
}
