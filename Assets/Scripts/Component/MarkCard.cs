using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Component
{
    public class MarkCard : MonoBehaviour
    {
        public Text speechText;
        public Button playButton;
        public Button destroyButton;
        private AudioClip speechClip;
        private string currentTime;
        private string endTime;
        private List<string> tags;
        private List<Color> colors;
        public Transform[] cards;
        // Use this for initialization
        void Start()
        {
            
        }

        public void createSelf(List<string> tags, List<Color> colors, string speech, AudioClip clip)
        {
            int length = tags.Count;
            if(length > 6)
            {
                length = 6;
                Debug.Log("[MarkCard Create] Index too long");
            }
            if(length != colors.Count)
            {
                Debug.Log("[MarkCard Create] Index does not match");
            }

            for(int i = 0; i<length; i++)
            {
                cards[i].GetComponent<Image>().color = colors[i];
                cards[i].Find("Text").GetComponent<Text>().text = tags[i];
            }

            if (clip != null)
                speechClip = clip;
            else
                Debug.Log("[MarkCard Create] AudioClip does not exist");

            if (speech != null)
                speechText.text = speech;
            else
                Debug.Log("[MarkCard Create] speech text does not exist");

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}