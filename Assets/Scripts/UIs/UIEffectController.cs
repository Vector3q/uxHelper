using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIEffectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    public bool HoverToChangeAlpha;
    public bool HoverToDisplayCircleBG;
    private Transform circleBG;
    // Start is called before the first frame update
    void Start()
    {

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


    }

    // 当鼠标离开对象时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        if (HoverToChangeAlpha)
            buttonImage.color = new Vector4(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0.25f);
        if (HoverToDisplayCircleBG)
            circleBG.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
