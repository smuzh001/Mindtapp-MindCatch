using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MindTAPP.Unity.IFT;
using UnityEngine.UI.Extensions;
using UnityEngine.Events;
using System;

namespace MindTAPP.Unity.Versions.Professional
{
    public class PhotosScreen : MonoBehaviour
    {
        [SerializeField]
        private HorizontalScrollSnap photoDisplay;
        [SerializeField]
        private Image prefabImage;
        [SerializeField]
        private IPhotos photoService;
        [SerializeField]
        private StorePhoto startingPhoto;

        [SerializeField]
        private UnityEvent onDelete;
        [SerializeField]
        private UnityEvent onUndo;

        public UnityEvent OnDelete { get; private set; }
        public UnityEvent OnUndo { get; private set; }

        // Holds undo information
        private GameObject recentlyDeleted;
        private int deletedIndex;

        private void Awake()
        {
            OnDelete = onDelete;
            OnUndo = onUndo;
        }

        private void Start()
        {
            GameObject startingPage = null;
            foreach (Sprite photoSprite in this.photoService.GetPhotos())
            {
                GameObject newPhoto = Instantiate(this.prefabImage.gameObject);
                newPhoto.GetComponent<Image>().sprite = photoSprite;
                this.photoDisplay.AddChild(newPhoto);
                if (startingPhoto.Photo == photoSprite)
                {
                    Debug.Log("Match found.");
                    startingPage = newPhoto;
                }
            }
            if (startingPage)
            {
                photoDisplay.StartingScreen = startingPage.transform.GetSiblingIndex();
            }
        }

        private void OnDisable()
        {
            DeletePhotoPermanently();
        }

        // Delete last removed photo permanently
        private void DeletePhotoPermanently()
        {
            if (recentlyDeleted)
            {
                Sprite permanentDeletion = recentlyDeleted.GetComponent<Image>().sprite;
                photoService.DeletePhoto(permanentDeletion);
            }
        }

        public void DeletePhoto()
        {
            if (photoDisplay.ChildObjects.Length <= 0)
            {
                return;
            }
            // Moves child screen outside view
            GameObject removedPhoto;
            deletedIndex = this.photoDisplay.CurrentPage;
            this.photoDisplay.RemoveChild(deletedIndex, out removedPhoto);
            // Prevents bugs / errors
            this.photoDisplay.UpdateLayout();
            // Delete old photos permanently
            DeletePhotoPermanently();
            // Put deleted photo in temporary storage
            recentlyDeleted = removedPhoto;
            // Event invoke
            OnDelete.Invoke();
        }

        public void UndoDeletion()
        {
            if (recentlyDeleted != null)
            {
                // Restores photo to previous position
                this.photoDisplay.AddChild(recentlyDeleted);
                this.photoDisplay.ChangePageOrder(recentlyDeleted.transform.GetSiblingIndex(), deletedIndex);
                // Jumps to the previously deleted photo
                this.photoDisplay.GoToScreen(deletedIndex);
                // Clear deletion
                recentlyDeleted = null;
                deletedIndex = -1;
                // Invoke event
                OnUndo.Invoke();
            }
        }
    }
}