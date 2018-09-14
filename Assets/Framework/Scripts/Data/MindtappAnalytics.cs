using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using UnityEngine.Analytics;
using Newtonsoft.Json.Bson;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System;

namespace MindTAPP.Unity.Data
{
    // Make sure this data corresponds to server representation
    // Spelling AND value
    public enum Games
    {
        Tappit = 3,
        TAPPinOrder = 5
    }

    public static class MindtappAnalytics
    {
        private static RestClient client = new RestClient("https://mindtapp.com");

        // Need to have a game session on server beforehand or will encounter server 500 error
        // Records game data, an example is CustomEvent(<insert auth token>, Games.Tappit, "Score", 34)
        // This would record a score of 34 under the game stats for tappit
        public static void CustomEvent(string token, Games game, string eventName, string data)
        {
            var request = new RestRequest("api/stats/", Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Authorization", "Token " + token);
            
            request.AddJsonBody(
                new
                {
                    type = eventName,
                    data = data,
                    game = game
                });
            ServicePointManager.ServerCertificateValidationCallback = DataUtility.MyRemoteCertificateValidationCallback;
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                Debug.Log("Failed to start new session: " + response.ErrorMessage);
            }
            Debug.Log(response.Content);
        }

        // Returns user access to the game, true if can access
        public static bool GetGamePermission(string token, Games game)
        {
            var request = new RestRequest("api/access/{game}", Method.GET);
            request.AddParameter("game", game.ToString(), ParameterType.UrlSegment);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Authorization", "Token " + token);

            ServicePointManager.ServerCertificateValidationCallback = DataUtility.MyRemoteCertificateValidationCallback;
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                Debug.Log("Failed to start new session: " + response.ErrorMessage);
                return false;
            }
            Debug.Log(response.Content);
            JObject obj = JObject.Parse(response.Content);
            string permissionKey = obj.Properties().Select(p => p.Name).FirstOrDefault();
            return (permissionKey == "success");
        }

        // Registers a user under an access code group. Access code groups control which games their users can access
        public static void RegisterAccessCode(string token, string accessCode)
        {
            var request = new RestRequest("api/access/", Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Authorization", "Token " + token);
            request.AddJsonBody(
                new
                {
                    access_code = accessCode
                });
            ServicePointManager.ServerCertificateValidationCallback = DataUtility.MyRemoteCertificateValidationCallback;
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                Debug.Log("Failed to register access code: " + response.ErrorMessage);
            }
            Debug.Log(response.Content);
        }

        // @TODO, does not work correctly
        // Suppose to initialize the users data record into a game. Without this, can't record data for that game.
        // You specify the game by the game index, which you can find the key for in 'Games' enum.
        public static void NewSession(string token)
        {
            var request = new RestRequest("api/participant_register/", Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Authorization", "Token " + token);

            request.AddJsonBody(
                new
                {
                    game = 3,
                });
            ServicePointManager.ServerCertificateValidationCallback = DataUtility.MyRemoteCertificateValidationCallback;
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                Debug.Log("Failed to start new session: " + response.ErrorMessage);
            }
            else
            {
                Debug.Log(response.Content);
            }
        }

        // Does what it says
        public static void RegisterUser(string username, string password, 
            string email = "", int id = 0, string firstName = "", string lastName = "")
        {
            var request = new RestRequest("api/register/", Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(
                new
                {
                    id = id.ToString(),
                    first_name = firstName,
                    last_name = lastName,
                    email = email,
                    password = password,
                    username = username
                });
            ServicePointManager.ServerCertificateValidationCallback = DataUtility.MyRemoteCertificateValidationCallback;
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                Debug.Log("Failed to register user: " + response.ErrorMessage);
            }
        }

        // Returns authorization token
        // This token is needed to access the other functions
        // Store token responsibly
        public static string Authenticate(string username, string password)
        {
            var request = new RestRequest("api/api-auth/", Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(
                new
                {
                    username = username,
                    password = password
                });
            
            ServicePointManager.ServerCertificateValidationCallback = DataUtility.MyRemoteCertificateValidationCallback;
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                Debug.Log("Failed to authenticate user: " + response.ErrorMessage);
                return null;
            }
            else
            {
                // Debug.Log(response.Content);
                JObject tokenJson = JObject.Parse(response.Content);
                // Debug.Log(tokenJson["token"].ToString());
                return tokenJson["token"].ToString();
            }
        }
    }
}