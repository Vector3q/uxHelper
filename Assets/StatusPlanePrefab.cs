using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPlanePrefab : MonoBehaviour
{
    public Transform[] statusInfo;
    public RcmdNetwork rcmdNetwork;
    // Start is called before the first frame update
    void Start()
    {
        rcmdNetwork = transform.parent.GetComponent<MarkCard>().rcmdNetwork;
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
            string tagi = lines[i];
            Color colori = Utils.Utils.FindColor(tagi);
            statusInfo[i].GetComponent<Image>().color = colori;
            Transform planeTrans = statusInfo[i].Find("Panel");
            planeTrans.GetComponent<Image>().color = colori;
            Transform TextTrans = planeTrans.Find("Text");
            TextTrans.GetComponent<Text>().text = tagi;
            
            statusInfo[i].gameObject.SetActive(true);

            
        }

        for(int i=0; i<count; i++)
        {
            string tagi = lines[i];
            Debug.Log($"[MarkNode] {tagi} {statusInfo[i].name}");
            if (statusInfo[i].gameObject != null)
            {
                rcmdNetwork.AddMarkNode(tagi, statusInfo[i]);
            }
        }
        rcmdNetwork.PrintMarkNetwork();
    }
}
