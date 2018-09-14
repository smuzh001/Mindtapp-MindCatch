/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections;
using System.IO;
using System;

using UnityEngine;
using UnityEngine.UI;
using MindTAPP.Unity.IFT;
using UnityEngine.Events;

namespace MindTAPP.Unity.Versions.Professional
{
    // Attach to camera object w/ active render texture
    [RequireComponent(typeof(OrientateVirtualCamera))]
    public class CameraShot : MonoBehaviour
    {
        [SerializeField]
        private IPhotos gallery;
        [SerializeField]
        private UnityEvent onTookPhoto;

        public UnityEvent OnTookPhoto
        {
            get { return onTookPhoto; }
            private set { onTookPhoto = value; }
        }
        public Sprite PreviousPhoto { get; private set; }

        private Camera photoCamera; // Reference to camera that we will take pixels of
        private RenderTexture cameraFeed; // Texture that contains camera feed

        // Requires attached camera to have render texture
        private void Awake()
        {
            this.photoCamera = GetComponent<Camera>();
            this.cameraFeed = photoCamera.targetTexture;
        }

        private string CreateUniqueFileName(string fileExtension)
        {
            return "Photo " + DateTime.Now.ToString("__yyyy-MM-dd__HH-mm-ss.fff_tt") + fileExtension;
        }

        // Saves photo and send the sprite to photoPreview.
        // If photo preview is null, camera behaves normally
        // and saves the image to a file
        public void CaptureCameraShot()
        {
            string fileName = CreateUniqueFileName(".png");
            StartCoroutine(ProcessPhoto(fileName));
        }

        // Takes photo and converts it to an image of bytes, then saves it to the indicated file name.
        private IEnumerator ProcessPhoto(string fileName)
        {
            // Wait for end of frame to ensure quality picture
            yield return new WaitForEndOfFrame();
            //this.photoCamera.targetTexture = this.cameraFeed;
            RenderTexture.active = this.cameraFeed;
            // Read in pixels
            Texture2D photoTexture2D = new Texture2D(this.cameraFeed.width, this.cameraFeed.height);
            Rect photoDimensions = new Rect(0, 0, cameraFeed.width, cameraFeed.height);
            photoTexture2D.ReadPixels(photoDimensions, 0, 0);
            photoTexture2D.Apply();
            // Fixes render bug/error
            RenderTexture.active = null;
            // Generates sprite from photo
            Sprite spriteOfPhoto = Sprite.Create(photoTexture2D, photoDimensions, new Vector2());
            // Save photo to chosen path
            gallery.AddPhoto(spriteOfPhoto,  fileName);
            PreviousPhoto = spriteOfPhoto;
            OnTookPhoto.Invoke();
            // Log save
            Debug.Log("Photo Saved");
        }
    }
}