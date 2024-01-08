using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RcmdFilter : MonoBehaviour
{
    public RcmdNetwork rcmdNetwork;
    public Button filterButton;
    private bool isClick = true;
    private string tag;
    // Start is called before the first frame update
    void Start()
    {
        tag = transform.Find("Text").GetComponent<Text>().text;
        filterButton.onClick.AddListener( ()=> 
            { 
                rcmdNetwork.ControlMarkNetwork(tag, isClick);
                isClick = !isClick;
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
