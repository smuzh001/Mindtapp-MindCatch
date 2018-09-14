using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MindTAPP.Unity.Framework
{
    [RequireComponent(typeof(Text))]
    public class TextLives : ILives
    {
        private Text livesText;

        private void Awake()
        {
            livesText = GetComponent<Text>();
        }

        private void Start()
        {
            livesText.text = numberOfLives.ToString();
        }

        public override void AddLives(int lives = 1)
        {
            if (lives <= 0)
            {
                return;
            }

            numberOfLives += lives;
            livesText.text = numberOfLives.ToString();
        }

        public override void LoseLives(int lives = 1)
        {
            if (lives <= 0)
            {
                return;
            }

            if (numberOfLives < lives)
            {
                numberOfLives = 0;
            }
            else
            {
                numberOfLives -= lives;
            }

            livesText.text = numberOfLives.ToString();
        }
    }
}