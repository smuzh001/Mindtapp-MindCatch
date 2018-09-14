using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.Versions.Professional
{
    public class ThumbnailManager : MonoBehaviour
    {
        [SerializeField]
        private Thumbnail thumbnail;
        [SerializeField]
        private IPhotos photos;
        private Stack<Thumbnail> recentlyDeleted;

        private void Awake()
        {
            recentlyDeleted = new Stack<Thumbnail>();
        }

        // Use this for initialization
        private void Start()
        {
            foreach (Sprite photo in photos.GetPhotos())
            {
                GameObject pic = Instantiate(thumbnail.gameObject, transform);
                Thumbnail created = pic.GetComponent<Thumbnail>();
                created.Photo = photo;
            }
        }

        private void DeletePhotosPermanently()
        {
            foreach (Thumbnail t in recentlyDeleted)
            {
                photos.DeletePhoto(t.Photo);
                Destroy(t.gameObject);
            }
            recentlyDeleted.Clear();
        }

        private void OnDisable()
        {
            DeletePhotosPermanently();
        }

        public void SelectAll(bool shouldSelectAll)
        {
            foreach (Thumbnail t in GetComponentsInChildren<Thumbnail>())
            {
                t.ToggleStatus = shouldSelectAll;
            }
        }

        public void DeletionMode()
        {
            foreach (Thumbnail t in GetComponentsInChildren<Thumbnail>(true))
            {
                t.ToggleMode();
            }
        }

        public void SelectionMode()
        {
            foreach (Thumbnail t in GetComponentsInChildren<Thumbnail>(true))
            {
                t.ButtonMode();
            }
        }

        public void UndoDelete()
        {
            foreach (Thumbnail t in recentlyDeleted)
            {
                t.ToggleStatus = false;
                t.gameObject.SetActive(true);
            }
            recentlyDeleted.Clear();
        }

        public void DeleteSelected()
        {
            DeletePhotosPermanently();
            foreach (Thumbnail t in GetComponentsInChildren<Thumbnail>())
            {
                if (t.ToggleStatus)
                {
                    t.gameObject.SetActive(false);
                    recentlyDeleted.Push(t);
                }
            }
        }
    }
}