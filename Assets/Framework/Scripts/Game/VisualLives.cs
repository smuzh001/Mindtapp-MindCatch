using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MindTAPP.Unity.Framework
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class VisualLives : ILives
    {
        [SerializeField]
        private Sprite graphic;
        private GameObject lifeGraphic;

        private void Awake()
        {
            // Setup game object from graphic given
            lifeGraphic = new GameObject("Life");
            Image visual = lifeGraphic.AddComponent<Image>();
            visual.sprite = graphic;
        }

        private void Start()
        {
            for (int i = 0; i < numberOfLives; i++)
            {
                Instantiate(lifeGraphic, transform);
            }
        }

        public override void AddLives(int lives = 1)
        {
            if (lives <= 0)
            {
                return;
            }

            for (int i = 0; i < lives; i++)
            {
                Instantiate(lifeGraphic, transform);
            }
            numberOfLives += lives;
        }

        public override void LoseLives(int lives = 1)
        {
            if (lives <= 0)
            {
                return;
            }

            if (numberOfLives <= lives)
            {
                for (int i = 0; i < numberOfLives; i++)
                {
                    // Destroy happens end of frame, so we are not going out of bounds during destroy call
                    Destroy(transform.GetChild(i).gameObject);
                }
                numberOfLives = 0;
                onDeath.Invoke();
            }
            else
            {
                for (int i = 0; i < lives; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
                numberOfLives -= lives;
            }
        }
    }
}