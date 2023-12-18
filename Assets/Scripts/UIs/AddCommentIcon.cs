using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using HuggingFace.API;
using GPTIntergration;
public class AddCommentIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    // Start is called before the first frame update
    public Text text;
    public Text SpeechRecognitionText;
    public Image image;
    public Slider playSlider;
    public Sprite BeforeclickedSprite;
    public Sprite AfterclickedSprite;
    public GameObject textPanel;
    public RealTimeVoice rtv;
    public ChatGPTConversation agent;


    private AudioClip clip;
    private byte[] bytes;
    
    private bool IsClick;
    
    void Start()
    {
        IsClick = false;
        agent.SendToChatGPT("Hello");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(playSlider.handleRect.position.x, playSlider.handleRect.position.y, playSlider.handleRect.position.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = new Vector4(text.color.r, text.color.g, text.color.b, 1);
        image.color = new Vector4(image.color.r, image.color.g, image.color.b, 1);


    }

    // 当鼠标离开对象时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsClick == false)
        {
            text.color = new Vector4(text.color.r, text.color.g, text.color.b, 0.25f);
            image.color = new Vector4(image.color.r, image.color.g, image.color.b, 0.25f);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        IsClick = !IsClick;
        if (IsClick)
        {
            image.sprite = AfterclickedSprite;
            textPanel.SetActive(true);
        }
        else
        {
            image.sprite = BeforeclickedSprite;
        }
        Recording();
    }

    private void Recording()
    {
        if (IsClick)
        {
            rtv.StartASR();
            
        }
        else
        {
            rtv.StopASR();
        }
    }

    private IEnumerator ASR()
    {
        yield return 0;
        rtv.StartASR();
    }
}
