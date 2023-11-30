using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScissorsButton : MonoBehaviour
{
    public int CuttingState = 0;
    Button scissorsButton;
    CustomImage scissorsImageScript;
    Slider playSlider;
    // Start is called before the first frame update
    void Start()
    {
        scissorsButton = GetComponent<Button>();
        scissorsImageScript = GetComponent<CustomImage>();
        scissorsButton.onClick.AddListener(ScissorsButtonClick);
        playSlider = GetComponentInParent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScissorsButtonClick()
    {
        if (CuttingState == 0)
        {
            CuttingState++;
            scissorsImageScript.color = Color.green;
        }


        
    }


}
