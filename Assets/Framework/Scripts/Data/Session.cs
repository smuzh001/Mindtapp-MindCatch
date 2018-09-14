using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
namespace MindTAPP.Unity.Data
{
    public class Session : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            GameAnalytics.StartSession();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Login Time", System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Logout Time", System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
            }
        }
        

        private void OnApplicationQuit()
        {
            
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Logout Time", System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
            GameAnalytics.EndSession();
        }
    }
}