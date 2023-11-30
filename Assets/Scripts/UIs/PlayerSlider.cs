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
    void Start()
    {
        playSlider = GetComponent<Slider>();
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
            // ��ȡ Slider ��������� RectTransform
            RectTransform fillRect = fill.GetComponent<RectTransform>();
            Debug.Log("fillRect: "+ fillRect.position);

            // ʵ����Ԥ����
            GameObject newObject = Instantiate(fillPrefab);

            // ����������ĸ�����Ϊ Slider��ȷ�������� Slider �ƶ�
            newObject.transform.SetParent(playSlider.transform, false);
            newObject.GetComponent<RectTransform>().anchorMin = fillRect.anchorMin;
            newObject.GetComponent<RectTransform>().anchorMax = fillRect.anchorMax;
            //(fillRect.offsetMax.x - fillRect.offsetMin.x)
            newObject.GetComponent<RectTransform>().offsetMin = new Vector2(900.0f * startValue, fillRect.offsetMin.y);
            newObject.GetComponent<RectTransform>().offsetMax = new Vector2(-(900.0f - 900.0f * endValue), fillRect.offsetMax.y);
            Debug.Log("1: "+ fillRect.offsetMin + " " +fillRect.offsetMax);
            Debug.Log("2: " + newObject.GetComponent<RectTransform>().offsetMin + " " + newObject.GetComponent<RectTransform>().offsetMax);

            // �����������λ�����С���������һ��
            //newObject.transform.position = new Vector3((startValue+endValue) * fillRect.position.x / 2, fillRect.position.y, 0f);
            //newObject.transform.localScale = new Vector2(fillRect.sizeDelta.x * startValue, fillRect.sizeDelta.y * endValue);
        }
    }


}
