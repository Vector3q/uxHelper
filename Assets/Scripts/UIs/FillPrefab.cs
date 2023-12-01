using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FillPrefab : MonoBehaviour
{
    public Canvas thiscanvas;
    UIResize uIResize;
    public float startValue;
    public float endValue;
    // Start is called before the first frame update
    void Start()
    {
        Transform playSlider = transform.parent;
        Debug.Log("playSlider: " + playSlider.name);
        Transform videoScreen = playSlider.parent;
        Debug.Log("videoScreen: " + videoScreen.name);
        Transform canvas = videoScreen.parent;
        Debug.Log("canvas: " + canvas.name);
        uIResize = canvas.GetComponent<UIResize>();
        Debug.Log("this width: " + uIResize.width);
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("start-end: " + startValue + ", " + endValue);

        GetComponent<RectTransform>().offsetMin = new Vector2(uIResize.width * startValue, GetComponent<RectTransform>().offsetMin.y);
        GetComponent<RectTransform>().offsetMax = new Vector2(-(uIResize.width - uIResize.width * endValue), GetComponent<RectTransform>().offsetMin.y);
        Debug.Log("start: " + GetComponent<RectTransform>().offsetMin);
        Debug.Log("end: " + GetComponent<RectTransform>().offsetMax);
    }
}
