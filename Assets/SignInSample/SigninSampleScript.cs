﻿// <copyright file="SigninSampleScript.cs" company="Google Inc.">
// Copyright (C) 2017 Google Inc. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations

using System.Collections.Generic;
using System.Threading.Tasks;
using Google;
using TMPro;
using UnityEngine;

namespace SignInSample
{
    public class SigninSampleScript : MonoBehaviour
    {
        //public Text statusText;

        [SerializeField] private TextMeshProUGUI m_PlayerName;

        private GoogleSignInConfiguration configuration;

        // Defer the configuration creation until Awake so the web Client ID
        // Can be set via the property inspector in the Editor.
        private void Awake()
        {
          configuration = new GoogleSignInConfiguration
          {
                WebClientId = Constants.WEB_CLIENT_ID,
                RequestIdToken = true
          };
        }

        public void OnSignIn()
        {
          GoogleSignIn.Configuration = configuration;
          GoogleSignIn.Configuration.UseGameSignIn = false;
          GoogleSignIn.Configuration.RequestIdToken = true;
          GoogleSignIn.Configuration.RequestIdToken = true;
          //AddStatusText("Calling SignIn");

          GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        }

        public void OnSignOut()
        {
          //AddStatusText("Calling SignOut");
          GoogleSignIn.DefaultInstance.SignOut();
        }

        public void OnDisconnect()
        {
          //AddStatusText("Calling Disconnect");
          GoogleSignIn.DefaultInstance.Disconnect();
        }

        internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
        {
          if (task.IsFaulted)
          {
            using (IEnumerator<System.Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
              if (enumerator.MoveNext())
              {
                GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                //AddStatusText("Got Error: " + error.Status + " " + error.Message);
                UiManager.Instance.ChangeScreen(0);
              }
              //else
              //{
              //  AddStatusText("Got Unexpected Exception?!?" + task.Exception);
              //}
              UiManager.Instance.ChangeScreen(0);
            }
          }
          else if(task.IsCanceled)
          {
            //AddStatusText("Canceled");
            UiManager.Instance.ChangeScreen(0);
          }
          else
          {
            //AddStatusText("Welcome: " + task.Result.DisplayName + "!");
            m_PlayerName.text = task.Result.DisplayName;
            GameManager.Instance.StartGameplay();
          }
        }

        public void OnSignInSilently()
        {
          GoogleSignIn.Configuration = configuration;
          GoogleSignIn.Configuration.UseGameSignIn = false;
          GoogleSignIn.Configuration.RequestIdToken = true;
          //AddStatusText("Calling SignIn Silently");

          GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
        }


        public void OnGamesSignIn()
        {
          GoogleSignIn.Configuration = configuration;
          GoogleSignIn.Configuration.UseGameSignIn = true;
          GoogleSignIn.Configuration.RequestIdToken = false;

          //AddStatusText("Calling Games SignIn");

          GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        }

        private List<string> messages = new List<string>();
        //void AddStatusText(string text) {
        //  if (messages.Count == 5) {
        //    messages.RemoveAt(0);
        //  }
        //  messages.Add(text);
        //  string txt = "";
        //  foreach (string s in messages) {
        //    txt += "\n" + s;
        //  }
        //  statusText.text = txt;
        //}
  }
}