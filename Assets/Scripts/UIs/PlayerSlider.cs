using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainVideo;
    public Slider playSlider;
    public GameObject fillPrefab;
    public GameObject fill;
    public Canvas canvas;
    UIResize uIResize;
    void Start()
    {
        playSlider = GetComponent<Slider>();
        uIResize = canvas.GetComponent<UIResize>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSliderValue(float value)
    {
        playSlider.value = value;
    }

    public float getSliderValue()
    {
        return playSlider.value;
    }

    public void CreateFillPrefab(float startValue, float endValue)
    {
        if (playSlider != null && fillPrefab != null)
        {
            // 获取 Slider 的填充区域 RectTransform
            RectTransform fillRect = fill.GetComponent<RectTransform>();

            // 实例化预制体
            GameObject newObject = Instantiate(fillPrefab);

            // 设置新物体的父对象为 Slider，确保它跟随 Slider 移动
            newObject.transform.SetParent(playSlider.transform, false);
            newObject.GetComponent<RectTransform>().anchorMin = fillRect.anchorMin;
            newObject.GetComponent<RectTransform>().anchorMax = fillRect.anchorMax;
            //(fillRect.offsetMax.x - fillRect.offsetMin.x)
            newObject.GetComponent<RectTransform>().offsetMin = new Vector2(uIResize.width * startValue, fillRect.offsetMin.y);
            newObject.GetComponent<RectTransform>().offsetMax = new Vector2(-(uIResize.width - uIResize.width * endValue), fillRect.offsetMax.y);
            newObject.GetComponent<FillPrefab>().startValue = startValue;
            newObject.GetComponent<FillPrefab>().endValue = endValue;

            // 设置新物体的位置与大小与填充区域一致
            //newObject.transform.position = new Vector3((startValue+endValue) * fillRect.position.x / 2, fillRect.position.y, 0f);
            //newObject.transform.localScale = new Vector2(fillRect.sizeDelta.x * startValue, fillRect.sizeDelta.y * endValue);
        }
    }


}
