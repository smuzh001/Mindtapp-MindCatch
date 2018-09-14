using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Security;
using System;

namespace MindTAPP.Unity.IFT
{
    [CreateAssetMenu(fileName = "RemoteWordlist", menuName = "IFT/RemoteWordlist")]
    public class RemoteWordlist : IWordlist
    {
        private List<string> wordlist;
        [SerializeField]
        private LocalWordlist backupWordlist;

        private void Awake()
        {
            string jsonWordlist = GetJsonWordlist();
            if (jsonWordlist == null)
            {
                wordlist = new List<string>(backupWordlist.GetWords());
            }
            else
            {
                wordlist = GetWordlistFromJson(jsonWordlist);
            }
        }
        
        private string GetJsonWordlist()
        {
            // Send a REST request to get the json wordlist
            var client = new RestClient("https://mindtapp.com");
            var request = new RestRequest("api/wordlist/{listname}", Method.GET);
            request.AddParameter("listname", "PositiveIFT", ParameterType.UrlSegment);
            ServicePointManager.ServerCertificateValidationCallback = DataUtility.MyRemoteCertificateValidationCallback;
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                Debug.Log("Error getting wordlist: " + response.ErrorMessage);
                return null;
            }
            else
            {
                return response.Content;
            }
        }

        // Parse json wordlist to get actual wordlist
        private List<string> GetWordlistFromJson(string jsonWordlist)
        {
            JObject JWordlist = JObject.Parse(jsonWordlist);
            JArray IFTWords = (JArray)JWordlist["words"];
            List<string> wordlist = new List<string>(IFTWords.Count);
            foreach (string word in IFTWords)
            {
                wordlist.Add(word);
            }
            return wordlist;
        }

        public override ReadOnlyCollection<string> GetWords()
        {
            return wordlist.AsReadOnly();
        }
        public override string GetJson()
        {
            throw new NotImplementedException();
        }
    }
}