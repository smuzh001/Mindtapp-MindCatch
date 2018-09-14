using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MindTAPP.Unity.Framework
{
    [RequireComponent(typeof(Text))]
    public class ScoreTracker : MonoBehaviour
    {
        private Text scoreText;
        private int score;

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                if (scoreText.enabled)
                {
                    scoreText.text = "Score: " + score;
                }
            }
        }

        private void Awake()
        {
            scoreText = GetComponent<Text>();
        }

        private void Start()
        {
            scoreText.text = "Score: 0";
        }

        public void SetVisible(bool isVisible)
        {
            scoreText.enabled = isVisible;
            if (isVisible)
            {
                scoreText.text = "Score: " + score;
            }
        }
    }
}