using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{

    #region parameter
    // videoPlayer
    public VideoPlayer videoPlayer;
    // rate to change speed (Abandon)
    public float speedChangeRate = 0.1f;
    // speed of playing video
    public float playbackSpeed;
    // each skip time
    private float skipTime = 10.0f;
    // total time of current video duration
    private float videoDuration;
    private float changedSecond;
    private float subTime;
    


    #endregion

    #region UI Related
    // Button image
    public Sprite playIcon;
    public Sprite stopIcon;
    public Image playButtonImage;

    // formatted time text
    public Text TimeText;
    public Slider volumeSlider;
    // playback speed drop down
    public Dropdown pbSpeedDropDown;
    public Slider playSlider;
    public Button scissorsButton;
    PlayerSlider playerSlider;
    #endregion





    // Start is called before the first frame update
    void Start()
    {
        changedSecond = 0f;
        videoPlayer = GetComponent<VideoPlayer>();
        pbSpeedDropDown.onValueChanged.AddListener(SelectPlaybackSpeed);
        playerSlider = playSlider.GetComponent<PlayerSlider>();
        playSlider.onValueChanged.AddListener( value => { if (subTime > 0.005f)  videoPlayer.time = value * videoDuration;});
        Setup();
        
    }

    // Update is called once per frame
    void Update()
    {
        updateTimeText();

        //videoPlayer.time = playerSlider.getSliderValue() * videoDuration;
        subTime = Mathf.Abs(playerSlider.getSliderValue() - changedSecond);


        if (subTime < 0.005f )
            playerSlider.setSliderValue((float)videoPlayer.time / videoDuration);
        changedSecond = playerSlider.getSliderValue();
    }

    // Set initial parameters
    void Setup()
    {
        videoPlayer.Pause();
        playbackSpeed = videoPlayer.playbackSpeed;
        videoDuration = (float)videoPlayer.clip.length;
        //Debug.Log("videoPlayer.GetDirectAudioVolume(0)" + videoPlayer.GetDirectAudioVolume(0));
        //volumeSlider.value = videoPlayer.GetDirectAudioVolume(0);
        //Debug.Log("Volume: " + volumeSlider.value);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    // As name
    void updateTimeText()
    {
        float currentTimeF = (float)videoPlayer.time;

        string currentTimeS = FormatTime(currentTimeF);

        string totalTimeS = FormatTime(videoDuration);

        string formattedTime = currentTimeS + " / " + totalTimeS;

        TimeText.text = formattedTime;
    }

    // As name
    void OnVolumeChanged(float volume)
    {
        SetVolume(volume);
    }

    // Set volume
    void SetVolume(float volume)
    {
        float mappedVolume = Mathf.Clamp01(volume);
        videoPlayer.SetDirectAudioVolume(0, mappedVolume);
    }

    string FormatTime(float length)
    {
        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                            Mathf.FloorToInt(length / 3600),
                                            Mathf.FloorToInt((length % 3600) / 60),
                                            Mathf.FloorToInt(length % 60));

        return formattedTime;
    }

    // Play or Pause the video, interact with PlayPause Button
    public void VideoPlayStop()
    {
        Debug.Log("STATUS: " + GetVideoStatus());
        if(GetVideoStatus() == true)
        {
            videoPlayer.Pause();
            playButtonImage.sprite = playIcon;
        }
        else
        {
            videoPlayer.Play();
            playButtonImage.sprite = stopIcon;
        }
    }
    // skip forward 10s
    public void SkipForward()
    {
        double newTime = videoPlayer.time + skipTime;
        newTime = Mathf.Clamp((float)newTime, 0.0f, videoDuration);
        SetPlayTime(newTime);
    }
    bool GetVideoStatus()
    {
        return videoPlayer.isPlaying;
    }
    // skip back 10s 
    public void SkipBack()
    {
        double newTime = videoPlayer.time - skipTime;
        newTime = Mathf.Clamp((float)newTime, 0.0f, (float)videoDuration);
        SetPlayTime(newTime);
    }

    // Listen to change of dropdown and Change the speed of playback
    void SelectPlaybackSpeed(int index)
    {
        if(pbSpeedDropDown.options[index].text == "normal")
        {
            videoPlayer.playbackSpeed = 1;
            return;
        }
        float newplaybackSpeed = float.Parse(pbSpeedDropDown.options[index].text);
        SetPlaybackSpeed(newplaybackSpeed);
    }

    // Set playback speed 
    void SetPlaybackSpeed(float newPlaybackSpeed)
    {
        videoPlayer.playbackSpeed = newPlaybackSpeed;
        playbackSpeed = newPlaybackSpeed;
        videoPlayer.playbackSpeed = Mathf.Clamp(videoPlayer.playbackSpeed, 0.1f, 3.0f);
    }

    // Set Video Current Time
    void SetPlayTime(double newTime)
    {
        videoPlayer.time = newTime;
    }

    

    
}
