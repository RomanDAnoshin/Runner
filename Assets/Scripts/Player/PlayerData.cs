﻿using Character;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities.SimpleJSON;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        public Action PlayerNameChanged;
        public Action CoinsChanged;
        public Action DistanceChanged;
        public Action CurrentDistanceChanged;

        [SerializeField] private string playerName;
        [HideInInspector] public string PlayerName
        {
            get {
                return playerName;
            }
            set {
                playerName = value;
                PlayerNameChanged?.Invoke();
            }
        }

        [SerializeField] private int coins;
        [HideInInspector] public int Coins
        {
            get {
                return coins;
            }
            set {
                coins = value;
                CoinsChanged?.Invoke();
            }
        }

        [SerializeField] private int distance;
        [HideInInspector] public int Distance
        {
            get {
                return distance;
            }
            set {
                distance = value;
                DistanceChanged?.Invoke();
            }
        }

        [SerializeField] private int currentDistance;
        [HideInInspector] public int CurrentDistance
        {
            get {
                return currentDistance;
            }
            set {
                currentDistance = value;
                CurrentDistanceChanged?.Invoke();
            }
        }

        void Start()
        {
            if (SceneManager.GetActiveScene().name == "Road") {
                var characterBodyCollision = FindObjectOfType<CharacterBodyCollision>();
                characterBodyCollision.CollisionCoin += OnCharacterCollisionCoin;
            }
            if (HasSaved()) {
                Load();
            }
        }

        public void Save()
        {
            var jsonObject = ToJSONObject();
            PlayerPrefs.SetString("PlayerData", jsonObject.ToString());
        }

        public bool HasSaved()
        {
            return PlayerPrefs.HasKey("PlayerData");
        }

        public void Load()
        {
            var data = PlayerPrefs.GetString("PlayerData");
            var jsonObject = JSON.Parse(data);
            PlayerName = jsonObject["PlayerName"].Value;
            Coins = int.Parse(jsonObject["Coins"].Value);
            Distance = int.Parse(jsonObject["Distance"].Value);
            CurrentDistance = 0;
        }

        public void Reset()
        {
            PlayerName = "";
            Coins = 0;
            Distance = 0;
            CurrentDistance = 0;

            Save();
        }

        private JSONNode ToJSONObject()
        {
            var result = new JSONObject();
            result.Add("PlayerName", PlayerName);
            result.Add("Coins", Coins);
            Distance += CurrentDistance;
            result.Add("Distance", Distance);
            return result;
        }

        private void OnCharacterCollisionCoin()
        {
            Coins++;
        }
    }
}