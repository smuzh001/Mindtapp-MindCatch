using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MindTAPP.Unity.IFT;
using Zenject;
namespace MindTAPP.Unity.Utilities
{
    public class LoadSceneOnClick : MonoBehaviour
    {
        public Texture2D fadeOutTexture;
        public float fadeSpeed = 0.1f;

        private int drawDepth = -1000;
        private float alpha = 1.0f;
        private int fadeDir = -1;

        private IPhotos photoAlbum;
        [Inject]
        public void Init(IPhotos photoAlbum, IWordlist IftList)
        {
            this.photoAlbum = photoAlbum;
            
        }

        private void OnGUI()
        {
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            BeginFade(-1);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public float BeginFade(int Direction)
        {
            fadeDir = Direction;
            return fadeSpeed;
        }
        [SerializeField]
        GameObject warning;
        RandomList<Sprite> photos;
        private void Start()
        {
            
            photos = new RandomList<Sprite>(photoAlbum.GetPhotos());
        }
        public void ConditionalFadeIntoScene(string sceneName)
        {
            
            if (photos.Size() == 0)
            {
                Debug.Log(photos.Size());
                warning.SetActive(true);
            }
            else
            {
                StartCoroutine(FadeScene(sceneName));
            }
        }
        public void FadeIntoScene(string sceneName)
        {
            StartCoroutine(FadeScene(sceneName));
        }

        public void LoadByName(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        IEnumerator FadeScene(string name)
        {
            yield return new WaitForSeconds(BeginFade(1));
            SceneManager.LoadScene(name);
        }
    }
}