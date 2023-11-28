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
        switch (uIResize.cornerType)
        {
            case 0:
                break;
            case 1:
                uIResize.exceedLimitSize(eventData.delta);
                //rect.offsetMax = rect.offsetMax + eventData.delta;
                //Debug.Log("rect.offsetMax: " + rect.offsetMax);
                break;

            case 2:
                rect.offsetMax = new Vector2(rect.offsetMax.x + eventData.delta.x, rect.offsetMax.y);
                rect.offsetMin = new Vector2(rect.offsetMin.x, rect.offsetMin.y + eventData.delta.y);
                break;
            case 3:
                rect.offsetMax = rect.offsetMax + eventData.delta;
                //Debug.Log("rect.offsetMax: " + rect.offsetMax);
                break;
            case 4:
                rect.offsetMax = rect.offsetMax + eventData.delta;
                //Debug.Log("rect.offsetMax: " + rect.offsetMax);
                break;

        }
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
