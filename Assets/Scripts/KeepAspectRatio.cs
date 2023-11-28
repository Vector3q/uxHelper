using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAspectRatio : MonoBehaviour
{
    public float aspectRatio = 16.0f / 9f;

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
            // ��ȡ������Ŀ��Ⱥ͸߶�
            float parentWidth = parentRectTransform.rect.width;
            float parentHeight = parentRectTransform.rect.height;

            // �����µĿ��Ⱥ͸߶ȣ����ֳ�����
            float newWidth = parentWidth;
            float newHeight = parentWidth / aspectRatio;

            if (newHeight > parentHeight)
            {
                newHeight = parentHeight;
                newWidth = parentHeight * aspectRatio;
            }

            // ����������Ĵ�С
            rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }
    }
}
