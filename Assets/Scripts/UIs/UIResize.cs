using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIResize : MonoBehaviour
{
    private RectTransform rect;//拖拽对象的矩形对象
    public Button topLeftButton;
    public Button topRightButton;
    public Button bottomLeftButton;
    public Button bottomRightButton;
    public int cornerType = 0;

    public float maxWidth = 1800;
    public float maxHeight = 1200;

    public float minWidth = 150;
    public float minHeight = 100;

    private int limitStatus = 0;
    private Vector2 changedRectMax;
    private Vector2 changedRectMin;
    void Start()
    {
        rect = GetComponent<RectTransform>();

    }

    public void ResizeCanvas(Vector2 deltaData)
    {
        switch (cornerType)
        {
            case 0:
                break;
            case 1:
                changedRectMax = rect.offsetMax + deltaData / 2;

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

                //
                if (deltaData.x < -500.0f)
                {
                    limitStatus++;
                    changedRectMax.x = maxWidth / 2;
                    limitStatus++;
                    changedRectMax.y = maxHeight / 2;
                }
                rect.offsetMax = changedRectMax;
                break;

            case 2:
                
                break;
            case 3:
                
                break;
            case 4:
                changedRectMin = rect.offsetMin + deltaData / 2;
                Debug.Log("changedRectMin: " + changedRectMin);
                Debug.Log("deltaData: " + deltaData);

                if (changedRectMin.x > -minWidth / 2)
                {
                    limitStatus++;
                    changedRectMin.x = -minWidth / 2;
                }
                else if (changedRectMin.x < -maxWidth / 2)
                {
                    limitStatus++;
                    changedRectMin.x = -maxWidth / 2;
                }
                if (changedRectMin.y > -minHeight / 2)
                {
                    limitStatus++;
                    changedRectMin.y = -minHeight / 2;
                }
                else if (changedRectMin.y < -maxHeight / 2)
                {
                    limitStatus++;
                    changedRectMin.y = -maxHeight / 2;
                }

                //
                if (deltaData.x < -500.0f || deltaData.x> 500.0f)
                {
                    limitStatus++;
                    changedRectMin.x = -maxWidth / 2;
                    limitStatus++;
                    changedRectMin.y = -maxHeight / 2;
                }
                rect.offsetMin = changedRectMin;
                Debug.Log("changedRectMinFinal: " + changedRectMin);

                break;

        }
        


       

    }
}
