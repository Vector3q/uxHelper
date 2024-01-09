using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MarkCard : MonoBehaviour
{
        
    public Text speechText;
    public Button playButton;
    public Button MarkIconButton;
    public GameObject MarkCardObject;
    public Transform StatusPlane;
    public AudioSource audioSource;
    public RcmdNetwork rcmdNetwork;
    private AudioClip speechClip;
    private string currentTime;
    private string endTime;
    private float sliderRatio;

        [HideInInspector]
    public List<string> tags;
    
    private List<Color> colors;
    public Transform[] cards;
    // Use this for initialization
    void Start()
    {
        MarkIconButton.onClick.AddListener( () => { MarkCardObject.SetActive(!GetMarkCardActive()); });
        playButton.onClick.AddListener( () => {audioSource.Play(); });
        rcmdNetwork = transform.parent.GetComponent<SliderAreaController>().rcmdNetwork;

        if(rcmdNetwork != null)
        {
            Debug.Log("[MarkCard] rcmdNetwork exist");
        }
        StatusPlane.GetComponent<StatusPlanePrefab>().rcmdNetwork = rcmdNetwork;
    }

        /// <summary>
        /// Create a new Mark
        /// </summary>
        /// <param name="tags">all tag</param>
        /// <param name="speech">transcript</param>
        /// <param name="clip">voice clip</param>
        public void createSelf(string[] tags, string speech, AudioClip clip)
        {
            int length = tags.Length;
            if(length > 6)
            {
                length = 6;
                Debug.Log("[MarkCard Create] Index too long");
            }

            for(int i = 0; i<length; i++)
            {
                Color colori = Utils.Utils.FindColor(tags[i]);
                cards[i].GetComponent<Image>().color = colori;
                cards[i].Find("Text").GetComponent<Text>().text = tags[i];
                cards[i].gameObject.SetActive(true);
            }

            if (clip != null) 
            {
                speechClip = clip;
                audioSource.clip = clip;
            }
                
            else
                Debug.Log("[MarkCard Create] AudioClip does not exist");

            if (speech != null)
                speechText.text = speech;
            else
                Debug.Log("[MarkCard Create] speech text does not exist");

        StartCoroutine(setStatusBar(tags));
        }

    IEnumerator setStatusBar(string[] tags)
    {
        yield return new WaitForSeconds(0.1f);
        StatusPlane.GetComponent<StatusPlanePrefab>().setStatusBar(tags);
    }

        // Update is called once per frame
        void Update()
        {
            
        }

        public bool GetMarkCardActive()
        {
            return MarkCardObject.activeSelf;
        }
        public void DeleteTag(string tag)
        {
            int index = tags.IndexOf(tag);

            if(index != -1)
            {
                tags.RemoveAt(index);
            }
            else
            {
                Debug.Log($"[MarkCard] No tag {tag}");
            }
        }
}