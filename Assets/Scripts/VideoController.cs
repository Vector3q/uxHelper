using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using Utils;
public enum VideoState
{
    normal,
    waitforinstruction,
    speaking,
    unactive,
}

public enum ClickState
{
    none,
    one,
    two,
}

public class VideoController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    #region parameter
    // videoPlayer
    public VideoPlayer videoPlayer;
    // rate to change speed (Abandon)
    public float speedChangeRate = 0.1f;
    // speed of playing video
    public float playbackSpeed;
    private VideoState videoState;
    private ClickState clickState;
    // each skip time
    private float doubleClickTimeThreshold = 0.4f;
    private float lastClickTime = 0f;
    private float skipTime = 10.0f;
    // total time of current video duration
    private float videoDuration;
    private float changedSecond;
    private float subTime;
    private float clickTime;
    private float clickCount;
    private float timerForHover;
    private bool isClicking = false;

    private Vector2 dragBeginPos;
    private Vector2 dragEndPos;
    private const float DRAGTHRESHOLD = 500f;



    #endregion

    #region UI Related
    // Button image
    public GameObject playButton;
    public GameObject textPanel;
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
    public Button addComment;

    PlayerSlider playerSlider;
    #endregion





    // Start is called before the first frame update
    void Start()
    {
        changedSecond = 0f;
        clickTime = 0f;
        clickCount = 0;
        videoPlayer = GetComponent<VideoPlayer>();
        pbSpeedDropDown.onValueChanged.AddListener(SelectPlaybackSpeed);
        addComment.onClick.AddListener(ClickToSpeak);
        playSlider.onValueChanged.AddListener(value => { if (subTime > 0.005f) videoPlayer.time = value * videoDuration; });
        Setup();

    }

    // Update is called once per frame
    void Update()
    {
        timerForHover += Time.deltaTime;
        
        updateTimeText();
        UpdateTimeSet();
        if (playSlider.gameObject.activeSelf == true)
        {
            playerSlider = playSlider.GetComponent<PlayerSlider>();
            //videoPlayer.time = playerSlider.getSliderValue() * videoDuration;
            subTime = Mathf.Abs(playerSlider.getSliderValue() - changedSecond);

            
            if (subTime < 0.005f)
                playerSlider.setSliderValue((float)videoPlayer.time / videoDuration);
            changedSecond = playerSlider.getSliderValue();
        }

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
        videoState = VideoState.normal;
        clickState = ClickState.none;

        
    }

    void UpdateTimeSet()
    {
        timego(ref clickTime);
        timego(ref timerForHover);
    }

    void timego(ref float targetTimer)
    {
        if (targetTimer < 60f)
        {
            targetTimer += Time.deltaTime;
        }
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

    void ClickToSpeak()
    {
        if (addComment.GetComponent<AddCommentIcon>().IsClick)
        {
            //videoState = VideoState.speaking;
        }
        else
        {
            //videoState = VideoState.waitforinstruction;
        }
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
        if (GetVideoStatus() == true)
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
        if (pbSpeedDropDown.options[index].text == "normal")
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

    public void OnPointerDown(PointerEventData eventData)
    {
        timerForHover = 0f;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (addComment.GetComponent<AddCommentIcon>().GetPlayStatus())
        {
            return;
        }



        if(clickTime > doubleClickTimeThreshold)
        {
            clickCount = 0;
            clickTime = 0f;
        }
        clickCount++;
        if (clickCount >= 3) clickCount = 1;
        



        if (videoState == VideoState.normal)
        {
            videoState = VideoState.waitforinstruction;
            StartWaitForInstruction();
        }

        if(videoState == VideoState.waitforinstruction)
        {
            if(clickCount == 2)
            {
                DoubleClickPlayorPause();
            }
        }



    }

    public void DoubleClickPlayorPause()
    {
        if (GetVideoStatus())
        {
            videoPlayer.Pause();
            playButton.SetActive(true);
            playButtonImage.sprite = playIcon;
        }
        else
        {
            videoPlayer.Play();
            playButton.SetActive(false);
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {


        if(timerForHover < 1f)
        {
            return;
        }
        //Debug.Log($"[Hover Exit] {timerForHover}");
        timerForHover = 0f;
        if (videoState == VideoState.waitforinstruction)
            StartCoroutine(Waitforinteraction());
    }


    IEnumerator Waitforinteraction()
    {
        yield return new WaitForSeconds(3f);
        videoState = VideoState.normal;
        EndWaitForInstruction();
    }
    private void StartWaitForInstruction()
    {

        addComment.gameObject.SetActive(true);

    }

    private void EndWaitForInstruction()
    {

        //addComment.gameObject.SetActive(false);
        textPanel.SetActive(false);

    }

    public void OnDrag(PointerEventData eventData)
    {
            
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(videoState == VideoState.waitforinstruction)
        {
            dragBeginPos = eventData.position;
            Debug.Log($"[Drag] drag begin position {dragBeginPos}");
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (videoState == VideoState.waitforinstruction)
        {
            dragEndPos = eventData.position;
            Debug.Log($"[Drag] drag end position {dragEndPos}");
            if(dragEndPos.x - dragBeginPos.x >= DRAGTHRESHOLD)
            {
                //SkipForward();
            }
            else if(dragEndPos.x - dragBeginPos.x <= -DRAGTHRESHOLD)
            {
                //SkipBack();
            }
        }
    }

}
