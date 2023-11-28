using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResizeButton : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    
    public Canvas canvas;
    UIResize uIResize;
    private RectTransform rect;

    public int thisType; 

    void Start()
    {
        uIResize = canvas.GetComponent<UIResize>();
        rect = canvas.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        uIResize.ResizeCanvas(eventData.delta);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        uIResize.cornerType = thisType;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        uIResize.cornerType = 0;
    }
}
