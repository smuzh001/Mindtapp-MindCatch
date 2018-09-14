using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MindTAPP.Unity.IFT;
using MindTAPP.Unity.Utilities;
using Zenject;
using UnityEngine.UI;
using System.IO;
public class EditWords : MonoBehaviour {
    //private IPhotos photoAlbum;
    //private IWordlist IftList;
    //RandomList<string> words;
    // Use this for initialization
    [Inject]
    public void Init(IPhotos photoAlbum, IWordlist IftList)
    {
        //this.photoAlbum = photoAlbum;
        //this.IftList = IftList;
    }
    void Start () {
        //photoAlbum.GetPhotos();
        //IftList.GetWords();
        //words = new RandomList<string>(IftList.GetWords());
        //InputField input = GameObject.Find("InputField").GetComponent<InputField>();
        //Text text = GameObject.Find("Text").GetComponent<Text>();
        //string path = Application.persistentDataPath;
        //input.text = IftList.GetJson();
        //StreamWriter file;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
