using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public GameObject playerPrefab;//自己
    public AudioSource myAudioSource;//自己的音源组件
    public float playTime;//去除尾音后音频的播放时间
    public bool looped = false;
    void Update()
    {
        //无缝叠加播放
        if (myAudioSource.time >= playTime && looped == false)
        {
            GameObject player = Instantiate(playerPrefab);
            player.name = "MusicPlayer";
            looped = true;
        }
        if (myAudioSource.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
