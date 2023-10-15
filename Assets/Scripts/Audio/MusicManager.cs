using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public static MusicManager Instance => instance;
    private AudioSource music;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        music = GetComponent<AudioSource>();
    }
    private void Start()
    {
        ChangeMusicPlay(DataManager.Instance.musicData.musicIsOpen);
        ChangeMusicValue(DataManager.Instance.musicData.musicValue);
    }
    public void ChangeMusicPlay(bool isPlay)
    {
        music.mute = !isPlay;
    }
    public void ChangeMusicValue(float value)
    {
        music.volume = value;
    }
}
