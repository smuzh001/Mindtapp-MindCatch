using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MindTAPP.Unity.Framework
{
    public abstract class ILives : MonoBehaviour
    {
        [SerializeField]
        protected int numberOfLives;

        [SerializeField]
        protected UnityEvent onDeath;
        public UnityEvent OnDeath { get; private set; }

        public int GetLives() { return numberOfLives; }
        public abstract void AddLives(int lives = 1);
        public abstract void LoseLives(int lives = 1);

        public void SetLives(int lives)
        {
            if (lives < 0)
            {
                return;
            }

            if (numberOfLives < lives)
            {
                AddLives(lives - numberOfLives);
            }
            else
            {
                LoseLives(numberOfLives - lives);
            }
        }
    }
}