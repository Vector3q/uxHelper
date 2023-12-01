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

    public float width;

    public float maxWidth = 1800;
    public float maxHeight = 1200;

    public float minWidth = 150;
    public float minHeight = 100;

    private int limitStatus = 0;
    private Vector2 changedRectMax;
    private Vector2 changedRectMin;
    private Vector2 changedRectMaxXMinY;
    private Vector2 changedRectMaxYMinX;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        width = rect.offsetMax.x * 2;
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
                if (deltaData.x < -500.0f || deltaData.x > 500.0f)
                {
                    limitStatus++;
                    changedRectMax.x = maxWidth / 2;
                    limitStatus++;
                    changedRectMax.y = maxHeight / 2;
                }
                rect.offsetMax = changedRectMax;
                
                break;

            case 2:
                changedRectMaxXMinY = new Vector2(rect.offsetMax.x + deltaData.x, rect.offsetMin.y + deltaData.y);

                if (changedRectMaxXMinY.x < minWidth / 2)
                {
                    limitStatus++;
                    changedRectMaxXMinY.x = minWidth / 2;
                }
                else if (changedRectMaxXMinY.x > maxWidth / 2)
                {
                    limitStatus++;
                    changedRectMaxXMinY.x = maxWidth / 2;
                }
                if (changedRectMaxXMinY.y > -minHeight / 2)
                {
                    limitStatus++;
                    changedRectMaxXMinY.y = -minHeight / 2;
                }
                else if (changedRectMaxXMinY.y < -maxHeight / 2)
                {
                    limitStatus++;
                    changedRectMaxXMinY.y = -maxHeight / 2;
                }

                //
                if (deltaData.x < -500.0f || deltaData.x > 500.0f)
                {
                    limitStatus++;
                    changedRectMaxXMinY.x = maxWidth / 2;
                    limitStatus++;
                    changedRectMaxXMinY.y = -maxHeight / 2;
                }

                rect.offsetMax = new Vector2(changedRectMaxXMinY.x, rect.offsetMax.y);
                rect.offsetMin = new Vector2(rect.offsetMin.x, changedRectMaxXMinY.y);
                break;
            case 3:
                changedRectMaxYMinX = new Vector2(rect.offsetMin.x + deltaData.x, rect.offsetMax.y + deltaData.y);

                if (changedRectMaxYMinX.x > -minWidth / 2)
                {
                    limitStatus++;
                    changedRectMaxYMinX.x = -minWidth / 2;
                }
                else if (changedRectMaxYMinX.x < -maxWidth / 2)
                {
                    limitStatus++;
                    changedRectMaxYMinX.x = -maxWidth / 2;
                }
                if (changedRectMaxYMinX.y < minHeight / 2)
                {
                    limitStatus++;
                    changedRectMaxYMinX.y = minHeight / 2;
                }
                else if (changedRectMaxYMinX.y > maxHeight / 2)
                {
                    limitStatus++;
                    changedRectMaxYMinX.y = maxHeight / 2;
                }

                //
                if (deltaData.x < -500.0f || deltaData.x > 500.0f)
                {
                    limitStatus++;
                    changedRectMaxYMinX.x = -maxWidth / 2;
                    limitStatus++;
                    changedRectMaxYMinX.y = maxHeight / 2;
                }

                rect.offsetMax = new Vector2(rect.offsetMax.x, changedRectMaxYMinX.y);
                rect.offsetMin = new Vector2(changedRectMaxYMinX.x, rect.offsetMin.y);
                break;
            case 4:
                changedRectMin = rect.offsetMin + deltaData / 2;

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
                break;

        }
        width = rect.offsetMax.x - rect.offsetMin.x;
    }
}
