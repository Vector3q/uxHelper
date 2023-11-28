using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResizeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Canvas canvas;
    UIResize uIResize;

    public int thisType; 

    void Start()
    {
        uIResize = canvas.GetComponent<UIResize>();
    }

    // Update is called once per frame
    void Update()
    {

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
