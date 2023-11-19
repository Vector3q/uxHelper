using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float speedChangeRate = 0.1f;
    private float playbackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        playbackSpeed = videoPlayer.playbackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Way to change playback speed: scroll 
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll != 0f)
        {
            AdjustPlaybackbyScroll(scroll);
        }
    }

    void AdjustPlaybackbyScroll(float scrollValue)
    {
        float newPlaybackSpeed = playbackSpeed + scrollValue * speedChangeRate;
        SetPlaybackSpeed(newPlaybackSpeed);
    }
       
    void SetPlaybackSpeed(float newPlaybackSpeed)
    {
        videoPlayer.playbackSpeed = newPlaybackSpeed;
        playbackSpeed = newPlaybackSpeed;
        videoPlayer.playbackSpeed = Mathf.Clamp(videoPlayer.playbackSpeed, 0.1f, 3.0f);
    }

}
