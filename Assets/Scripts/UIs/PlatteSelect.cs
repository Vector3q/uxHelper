using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatteSelect : MonoBehaviour
{
    private int selectedColor;
    public Sprite nonSelectedSprite;
    public Sprite selectedSprite;
    public Image FillPrefabImage;

    public Button[] colorButtons;
    private Button nowSelectedButton;
    // Start is called before the first frame update
    void Start()
    {
        nowSelectedButton = colorButtons[9];
        nowSelectedButton.GetComponent<Image>().sprite = selectedSprite;
        foreach (Button button in colorButtons)
        {
            button.onClick.AddListener(() => ButtonClick(button));
        }

    }
    void ButtonClick(Button clickedButton)
    {
        nowSelectedButton.GetComponent<Image>().sprite = nonSelectedSprite;

        clickedButton.GetComponent<Image>().sprite = selectedSprite;
        nowSelectedButton = clickedButton;
        FillPrefabImage.color = nowSelectedButton.image.color;
        // 在这里处理按钮点击事件
        Debug.Log("Button Clicked: " + clickedButton.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
