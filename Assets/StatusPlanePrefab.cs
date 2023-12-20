using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPlanePrefab : MonoBehaviour
{
    public Transform[] statusInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStatusBar(string[] lines)
    {
        int count = lines.Length;

        for(int i=0; i<count; i++)
        {
            Color colori = Utils.Utils.FindColor(lines[i]);
            statusInfo[i].GetComponent<Image>().color = colori;
            Transform planeTrans = statusInfo[i].Find("Panel");
            planeTrans.GetComponent<Image>().color = colori;
            Transform TextTrans = planeTrans.Find("Text");
            TextTrans.GetComponent<Text>().text = lines[i];

            statusInfo[i].gameObject.SetActive(true);
        }
    }
}
