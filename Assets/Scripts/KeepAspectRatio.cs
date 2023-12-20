using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAspectRatio : MonoBehaviour
{
    public float aspectRatio = 16.0f / 9f;
    public float width;
    public float height;
    private RectTransform rectTransform;
    private Vector2 originalSize;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();

        if (parentRectTransform != null)
        {
            // 获取父物体的宽度和高度
            float parentWidth = parentRectTransform.rect.width;
            float parentHeight = parentRectTransform.rect.height;

            // 计算新的宽度和高度，保持长宽比
            float newWidth = parentWidth;
            float newHeight = parentWidth / aspectRatio;

            if (newHeight > parentHeight)
            {
                newHeight = parentHeight * 0.8f;
                newWidth = parentHeight * aspectRatio;
            }

            width = newWidth;
            height = newHeight;

            // 设置子物体的大小
            rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }
    }
}

