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




    private Transform toolTip;
    private Transform dialogBox;
    private Transform TooltipPlatte;
    private Button deleteBtn;
    private Button closeBtn;
    private Button dialogBtn;
    private Button platteBtn;
    // Start is called before the first frame update
    void Start()
    {
        toolTip = transform.Find("Tooltip");
        TooltipPlatte = toolTip.Find("TooltipPlatte");
        dialogBtn = toolTip.Find("Dialog").GetComponent<Button>();
        platteBtn = toolTip.Find("Platte").GetComponent<Button>();
        dialogBox = toolTip.Find("DialogBox");
        closeBtn = dialogBox.Find("Close").GetComponent<Button>();
        deleteBtn = dialogBox.Find("Delete").GetComponent<Button>();
        deleteBtn.onClick.AddListener(DeleteSelf);
        closeBtn.onClick.AddListener(CloseDialogBox);
        platteBtn.onClick.AddListener(InvokePlatte);
        dialogBtn.onClick.AddListener(InvokeDialogBox);


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

    void DeleteSelf()
    {
        Destroy(this.gameObject);
    }

    void InvokePlatte()
    {
        dialogBox.gameObject.SetActive(false);
        TooltipPlatte.gameObject.SetActive(true);
    }

    void InvokeDialogBox()
    {
        dialogBox.gameObject.SetActive(true);
        TooltipPlatte.gameObject.SetActive(false);
    }

    void CloseDialogBox()
    {
        dialogBox.gameObject.SetActive(false);

    }
}
