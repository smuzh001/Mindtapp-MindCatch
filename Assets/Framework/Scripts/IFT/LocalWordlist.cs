/*
* Author(s): Joshua Beto
* Company: MindTAPP
*/

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json;

namespace MindTAPP.Unity.IFT
{
    [CreateAssetMenu(fileName = "LocalWordlist", menuName = "IFT/LocalWordlist")]
    public class LocalWordlist : IWordlist
    {
        [SerializeField]
        private TextAsset wordsJSON;
        private List<string> wordlist;

        public void Awake()
        {
            Debug.Log(wordsJSON.text);
            JArray parsedWords = JArray.Parse(wordsJSON.text);
            wordlist = new List<string>(parsedWords.Count);
            foreach(string word in parsedWords)
            {
                wordlist.Add(word);
            }
        }

        public override ReadOnlyCollection<string> GetWords()
        {
            return wordlist.AsReadOnly();
        }
        public override string GetJson()
        {
            JArray words = JArray.Parse(wordsJSON.text);
            return words.ToString();
        }
    }
}