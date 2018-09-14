using System.Collections;
using System.Collections.Generic;
using MindTAPP.Unity.Framework;
using UnityEngine;
using UnityEngine.UI;
using MindTAPP.Unity.Utilities;
using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.Versions.Professional
{
    [RequireComponent(typeof(Image))]
    public class Thumbnail : MonoBehaviour
    {
        [SerializeField]
        private StorePhoto photoPreview;
        [SerializeField]
        private IPhotos photoService;
        [SerializeField]
        private Toggle myToggle;
        [SerializeField]
        private Button myButton;
        private Image myPhoto;

        public Sprite Photo { get { return myPhoto.sprite; } set { myPhoto.sprite = value; } }
        public bool ToggleStatus { get { return myToggle.isOn; } set { myToggle.isOn = value; } }

        private void Awake()
        {
            myPhoto = GetComponent<Image>();
        }

        public void ToggleSelection(bool toggle)
        {
            myPhoto.color = toggle ? Color.gray : Color.white;
        }

        public void ToggleMode()
        {
            myToggle.gameObject.SetActive(true);
            myButton.gameObject.SetActive(false);
        }

        public void ButtonMode()
        {
            myToggle.gameObject.SetActive(false);
            myButton.gameObject.SetActive(true);
        }

        public void GoToPhoto()
        {
            LoadSceneOnClick sceneLoader = FindObjectOfType<LoadSceneOnClick>();
            photoPreview.Photo = myPhoto.sprite;
            sceneLoader.FadeIntoScene("Slides");
        }
    }
}