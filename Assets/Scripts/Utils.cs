using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Utils
{
    public class Utils
    {
        static public List<string> AllTags = new List<string>();
        static public List<Color> AllColors = new List<Color>();

        static Utils()
        {
            AllColors.Add(Color.red);
            AllColors.Add(Color.green);
            AllColors.Add(Color.blue);
            AllColors.Add(Color.yellow);
            AllColors.Add(Color.cyan);
            AllColors.Add(Color.magenta);
            AllColors.Add(new Color(1f, 0.647f, 0f)); // Orange
            AllColors.Add(new Color(0.545f, 0.271f, 0.075f)); // Brown
            AllColors.Add(new Color(0.863f, 0.078f, 0.235f)); // Crimson
            AllColors.Add(new Color(0.294f, 0.0f, 0.51f)); // Indigo
        }

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