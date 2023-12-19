using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Utils
{
    public class Utils
    {
        static List<string> AllTags = new List<string>();
        static public string[] TextClean(string messyText)
        {
            string[] lines = messyText.Split('\n');
            for(int i=0; i<lines.Length; i++)
            {
                bool isNew = true;
                lines[i] = lines[i].TrimStart('-', ' ');
                if (AllTags.Contains(lines[i]))
                {
                    isNew = false;
                }


                if (isNew)
                    AllTags.Add(lines[i]);
            }


            foreach(string tag in AllTags)
            {
                Debug.Log("AllTags:" + tag);
            }
            
            return lines;
        }
    }
}