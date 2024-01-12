using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class TestVideo : MonoBehaviour
{
    // Start is called before the first frame update
    VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = Application.streamingAssetsPath + "/" + "video_zhongyue.mp4";
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPrepared;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // 获取视频时长
        vp.Play();
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }
}
