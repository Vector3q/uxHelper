using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterAreaController : MonoBehaviour
{
    public GameObject FilterItemPrefab;
    private int ItemLength=0;
    

    public void ResetFilterItems(int newLength)
    {
        for(int i = 0; i< newLength; i++)
        {
            InvokeItem(Utils.Utils.AllTags[i], Utils.Utils.AllColors[i], i);
        }
        ItemLength = newLength;
    }

    private void InvokeItem(string text, Color color, int index)
    {
        string itemName = string.Format("FilterItem ({0})", index);

        Transform item = transform.Find(itemName);
        //GameObject newObject = Instantiate(FilterItemPrefab);
        //newObject.transform.parent = transform;

        if(item != null)
        {
            Debug.Log("[Filter] SUCCESS");

            Transform textTransform = item.transform.Find("Text");

            Transform imageTransform = item.transform.Find("Image");

            textTransform.GetComponent<Text>().text = text;
            imageTransform.GetComponent<Image>().color = color;

            item.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("[Filter] FAIL");
        }
        

        
    }

}
