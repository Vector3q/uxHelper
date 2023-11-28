using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIResize : MonoBehaviour, IDragHandler
{
    private RectTransform rect;//拖拽对象的矩形对象
    public Button topLeftButton;
    public Button topRightButton;
    public Button bottomLeftButton;
    public Button bottomRightButton;
    public int cornerType = 0;

    public float maxWidth = 1800;
    public float maxHeight = 1200;

    public float minWidth = 300;
    public float minHeight = 200;

    private int limitStatus = 0;
    private Vector2 changedRectMax;
    void Start()
    {
        rect = GetComponent<RectTransform>();

    }


    public void ResizeCanvasByMousePosition(Vector2 mousePosition)
    {
        // 设置Canvas的大小，使其与鼠标的位置相匹配
        rect.sizeDelta = new Vector2(Mathf.Abs(mousePosition.x) * 2, Mathf.Abs(mousePosition.y) * 2);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        switch (cornerType)
        {
            case 0:
                break;
            case 1:
                exceedLimitSize(eventData.delta);
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

    void exceedLimitSize(Vector2 deltaData)
    {
        limitStatus = 0;
        changedRectMax = rect.offsetMax + deltaData/2;

        


        if (changedRectMax.x < minWidth / 2)
        {
            limitStatus++;
            changedRectMax.x = minWidth / 2;
        }
        else if (changedRectMax.x > maxWidth / 2)
        {
            limitStatus++;
            changedRectMax.x = maxWidth / 2;
        }
        if (changedRectMax.y < minHeight / 2)
        {
            limitStatus++;
            changedRectMax.y = minHeight / 2;
        }
        else if (changedRectMax.y > maxHeight / 2)
        {
            limitStatus++;
            changedRectMax.y = maxHeight / 2;
        }
        rect.offsetMax = changedRectMax;
        Debug.Log("offsetMax:" + rect.offsetMax);

    }
}
