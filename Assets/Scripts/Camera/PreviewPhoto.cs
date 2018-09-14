using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MindTAPP.Unity.Utilities;

namespace MindTAPP.Unity.Versions.Professional
{
    [RequireComponent(typeof(Button))]
    public class PreviewPhoto : MonoBehaviour
    {
        [SerializeField]
        private Image myPhoto;
        [SerializeField]
        private StorePhoto photo;

        private CameraShot cameraShot;
        private Button myButton;

        private void Awake()
        {
            cameraShot = FindObjectOfType<CameraShot>();
            myButton = GetComponent<Button>();
        }

        private void Start()
        {
            myPhoto.enabled = true;
            myButton.interactable = true;
        }

        public void PreviewPhotoTaken()
        {
            myPhoto.enabled = true;
            myButton.interactable = true;
            myPhoto.sprite = cameraShot.PreviousPhoto;
        }

        public void GoToPhoto()
        {
            LoadSceneOnClick sceneLoader = FindObjectOfType<LoadSceneOnClick>();

            photo.Photo = myPhoto.sprite;
            sceneLoader.FadeIntoScene("Slides");
        }
    }
}