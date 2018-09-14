﻿/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace MindTAPP.Unity.IFT
{
    // Manages collection of photos currently saved and maintains a list of
    // Sprite objects for use in-game.
    [CreateAssetMenu(fileName = "FollowerAlbum", menuName = "IFT/FollowerAlbum")]
    public class PhotoGallery : IPhotos
    {
        private static bool isCreated;
        // Sprite is the visual representation of the photo and string is the file name.
        private static Dictionary<Sprite, string> photoFiles = new Dictionary<Sprite, string>();
        // Directory path that contains all photos.
        private static string DirectoryPath;
        
        private void OnEnable()
        {
            if (!isCreated)
            {
                // Creates photo directory if it does not exist and caches string path of photo directory
                DirectoryPath = Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Photos")).FullName;
                // Initializes photos in directory
                foreach (string filePath in Directory.GetFiles(DirectoryPath))
                {
                    Sprite photoSprite = CreatePhotoSprite(filePath);
                    photoFiles.Add(photoSprite, Path.GetFileName(filePath));
                }
                isCreated = true;
            }
        }

        // Be sure to check if filePath exist before actual use
        // Creates sprite from a given file path as long as it is in a supported file format
        private Sprite CreatePhotoSprite(string filePath)
        {
            byte[] photo;
            Texture2D textureForSprite = new Texture2D(2, 2);

            switch (Path.GetExtension(filePath).ToLower())
            {
                case ".jpeg":
                case ".jpg":
                case ".png":
                    photo = File.ReadAllBytes(filePath);
                    textureForSprite.LoadImage(photo);
                    break;
                case ".raw":
                    photo = File.ReadAllBytes(filePath);
                    textureForSprite.LoadRawTextureData(photo);
                    textureForSprite.Apply();
                    break;
                default:
                    Debug.LogWarning("Attempted to load data of unsupported file extension.");
                    return null; // Unsupported file type
            }

            return Sprite.Create(textureForSprite, new Rect(0, 0, textureForSprite.width, textureForSprite.height), new Vector2(0.5f, 0.5f));
        }

        public override void AddPhoto(Sprite photoToAdd, string fileName)
        {
            string filePath = Path.Combine(DirectoryPath, fileName);

            if (File.Exists(filePath)) // Prevent overwriting
            {
                Debug.LogError(fileName + " already exists in directory.");
            }
            else if (!photoToAdd)
            {
                Debug.LogError("Sprite object is null.");
            }
            else
            {
                Texture2D photoTexture = photoToAdd.texture;
                byte[] photoBytes;

                // Get byte[] data based on file extension
                switch (Path.GetExtension(filePath).ToLower())
                {
                    case ".jpeg":
                    case ".jpg":
                        photoBytes = ImageConversion.EncodeToJPG(photoTexture);
                        break;
                    case ".png":
                        photoBytes = ImageConversion.EncodeToPNG(photoTexture);
                        break;
                    case ".raw":
                        photoBytes = photoTexture.GetRawTextureData();
                        break;
                    default:
                        Debug.LogWarning("Could not identify useable file extension. Accepted file formats include : .jpg, .png, .raw.");
                        return;
                }
                // Add to Dictionary
                photoFiles.Add(photoToAdd, fileName);
                // Push write to disk on worker thread
                new System.Threading.Thread(() =>
                {
                    File.WriteAllBytes(filePath, photoBytes);
                }).Start();
            }
        }

        // Returns name of file that was deleted for undo purposes
        public override string DeletePhoto(Sprite photoToDelete)
        {
            string fileToDelete;

            if (photoFiles.TryGetValue(photoToDelete, out fileToDelete))
            {
                File.Delete(Path.Combine(DirectoryPath, fileToDelete));
                photoFiles.Remove(photoToDelete);
                Debug.Log("Deleted");
                return fileToDelete;
            }
            else
            {
                Debug.LogWarning("Attempted to delete a photo that does not exist. Make sure to use the given Sprite and not a copy.");
                return string.Empty;
            }
        }

        // Returns list of photos in Sprite format for use in-game.
        public override ReadOnlyCollection<Sprite> GetPhotos()
        {
            return photoFiles.Keys.ToList().AsReadOnly();
        }
        
        public Sprite GetSprite(string fileName)
        {
            return photoFiles.Single(item => item.Value.Equals(fileName)).Key;
        }

        public int GetPhotoCount()
        {
            return photoFiles.Count;
        }
    }
}