using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIEffectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    
    private Image buttonImage;
    public bool HoverToChangeAlpha;
    public bool HoverToChangeTextAlpha;
    public bool HoverToChangeAllChildrenAlpha;
    public bool HoverToDisplayCircleBG;
    public bool NoNeedToKeepClickStatus;
    public bool PointerUpToDisplay;
    public bool ClickToChangeIcon;
    public Sprite UnClickedIcon;
    public Sprite ClikedIcon;
    



    private Transform circleBG;
    private bool IsClick;
    private Transform textTransform;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        IsClick = false;
        if (HoverToChangeTextAlpha)
        {
            textTransform = transform.Find("Text");
            text = textTransform.GetComponent<Text>();
        }
        buttonImage = GetComponent<CustomImage>();

        if(buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
        }

        if (HoverToDisplayCircleBG)
        {
            circleBG = transform.Find("CircleBG");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(HoverToChangeAlpha)
            buttonImage.color = new Vector4(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1);
        if (HoverToDisplayCircleBG)
            circleBG.gameObject.SetActive(true);
        if (HoverToChangeTextAlpha)
            text.color = new Vector4(text.color.r, text.color.g, text.color.b, 1);

    }

    // 当鼠标离开对象时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        if (HoverToChangeAlpha && IsClick == false)
            buttonImage.color = new Vector4(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0.25f);
        if (HoverToDisplayCircleBG)
            circleBG.gameObject.SetActive(false);
        if (HoverToChangeTextAlpha)
            text.color = new Vector4(text.color.r, text.color.g, text.color.b, 0.25f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!NoNeedToKeepClickStatus)
        {
            IsClick = !IsClick;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.gameObject.SetActive(true);
    }
}
