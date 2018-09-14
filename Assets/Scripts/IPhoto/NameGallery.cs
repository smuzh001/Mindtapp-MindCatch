using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;
namespace MindTAPP.Unity.IFT
{
    public class NameGallery : MonoBehaviour
    {
        [SerializeField]
        private InputField textField;
        [SerializeField]
        Text preview;
        private void DisplayPreview(InputField input)
        {
            preview.text = input.text;
        }

        private void Awake()
        {
            textField = this.GetComponent<InputField>();
        }
        private void Submit(InputField input)
        {
            GAProgressionStatus name = GAProgressionStatus.Start;
            GameAnalytics.NewProgressionEvent(name, "Added Name: ", input.text);
            Debug.Log("Submitted");
            
            
        }
        public void Start()
        {
            textField.onValueChanged.AddListener(delegate { DisplayPreview(textField); });
            textField.onEndEdit.AddListener(delegate { Submit(textField); });

        }
    }
}
