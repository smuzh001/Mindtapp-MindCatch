using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace MindTAPP.Unity.Framework
{
    public class DisplayScores : MonoBehaviour
    {
        public List<Text> scores;

        // Use this for initialization
        private void Start()
        {
            string path = Path.Combine(Application.persistentDataPath, "Scores");
            if (File.Exists(path))
            {
                int i = 0;
                foreach (string sc in File.ReadAllLines(path))
                {
                    scores[i].text = (i + 1) + ". " + sc;
                    i++;
                }
            }
        }
    }
}