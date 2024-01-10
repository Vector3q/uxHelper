using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAreaController : MonoBehaviour
{
    public GameObject statusPrefab;
    public GameObject markCardPrefab;
    public Slider playSlider;
    public RcmdNetwork rcmdNetwork;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateStatusInfo(string[] lines)
    {
        if(statusPrefab != null)
        {
            RectTransform Rect = transform.GetComponent<RectTransform>();

            GameObject newObject = Instantiate(statusPrefab);

            newObject.transform.SetParent(transform, false);

            newObject.transform.position = new Vector3(playSlider.handleRect.position.x, Rect.position.y - 20f*0.002f, Rect.position.z);

            newObject.GetComponent<StatusPlanePrefab>().setStatusBar(lines);
        }
    }

    public void CreateMarkCard(string[] lines, string speech, AudioClip clip)
    {

        if(markCardPrefab != null)
        {
            RectTransform Rect = transform.GetComponent<RectTransform>();

            GameObject newObject = Instantiate(markCardPrefab);
            
            newObject.transform.SetParent(transform, false);

            newObject.transform.position = new Vector3(playSlider.handleRect.position.x, Rect.position.y + 80f * 0.002f, Rect.position.z);

            newObject.GetComponent<MarkCard>().createSelf(lines, speech, clip);
        }
    }
}
