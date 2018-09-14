using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UberAudio;

namespace MindTAPP.Unity.Versions.Professional
{
    public class TransitionModes : MonoBehaviour
    {
        [SerializeField]
        private GameObject optionsBar;
        [SerializeField]
        private GameObject selectionScreen;
        [SerializeField]
        private ThumbnailManager thumbnailManager;

        public void DeletionMode()
        {
            selectionScreen.SetActive(true);
            thumbnailManager.DeletionMode();
            AudioManager.Instance.Play("Click");
            optionsBar.SetActive(false);
        }

        public void SelectionMode()
        {
            optionsBar.SetActive(true);
            thumbnailManager.SelectionMode();
            AudioManager.Instance.Play("Click");
            selectionScreen.SetActive(false);
        }
    }
}