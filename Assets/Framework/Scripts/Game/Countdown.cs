using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UberAudio;

namespace MindTAPP.Unity.Framework
{
    [RequireComponent(typeof(Text))]
    public class Countdown : MonoBehaviour
    {
        [SerializeField]
        private string tickSound;

        private Text countdownText;
        public float StartingTime { get; set; }
        public float TimeLeft { get; private set; }

        private void Awake()
        {
            countdownText = GetComponent<Text>();
        }

        public void StartCountdown()
        {
            TimeLeft = StartingTime;
            AudioManager.Instance.Play(tickSound);
            countdownText.text = Mathf.CeilToInt(TimeLeft).ToString();
        }

        public void ResetCountdown()
        {
            countdownText.text = string.Empty;
        }

        public bool UpdateCountdown()
        {
            int previousCount = Mathf.CeilToInt(TimeLeft);
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0f)
            {
                countdownText.text = string.Empty;
                return true;
            }
            else
            {
                int currentCount = Mathf.CeilToInt(TimeLeft);
                if (previousCount != currentCount)
                {
                    AudioManager.Instance.Play(tickSound);
                    countdownText.text = Mathf.CeilToInt(TimeLeft).ToString();
                }
                return false;
            }
        }
    }
}