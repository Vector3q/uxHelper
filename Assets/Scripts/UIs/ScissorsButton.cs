using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScissorsButton : MonoBehaviour
{
    public int CuttingState = 0;
    Button scissorsButton;
    CustomImage scissorsImageScript;
    public float startValue;
    public float endValue;
    public Slider playSlider;
    public Image buttonImage;
    

    // Start is called before the first frame update
    void Start()
    {
        scissorsButton = GetComponent<Button>();
        scissorsImageScript = GetComponent<CustomImage>();
        scissorsButton.onClick.AddListener(ScissorsButtonClick);
        buttonImage = GetComponent<CustomImage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ScissorsButtonClick()
    {
        if (CuttingState == 0)
        {
            CuttingState=1;
            scissorsImageScript.color = Color.green;
            startValue = playSlider.value;
        }
        else if (CuttingState == 1)
        {
            CuttingState=0;
            scissorsImageScript.color = Color.white;
            endValue = playSlider.value; 
            Debug.Log("start-end: "+ startValue + ", " + endValue);
            playSlider.GetComponent<PlayerSlider>().CreateFillPrefab(startValue, endValue);
            Debug.Log("start-end: " + startValue + ", " + endValue);
        }
    }


}
