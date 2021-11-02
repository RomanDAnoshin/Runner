using Character;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities.SimpleJSON;

namespace Player
{
    public class PlayerData
    {
        public static PlayerData Instance
        {
            get {
                if(instance == null) {
                    instance = new PlayerData();
                }
                return instance;
            }
        }
        private static PlayerData instance;

        public Action PlayerNameChanged;
        public Action CoinsChanged;
        public Action CurrentCoinsChanged;
        public Action DistanceChanged;
        public Action CurrentDistanceChanged;
        public Action CurrentSpeedModificatorChanged;

        private string playerName;
        public string PlayerName
        {
            get {
                return playerName;
            }
            set {
                playerName = value;
                PlayerNameChanged?.Invoke();
            }
        }

        private int currentCoins;
        public int CurrentCoins
        {
            get {
                return currentCoins;
            }
            set {
                currentCoins = value;
                CurrentCoinsChanged?.Invoke();
            }
        }

        private int coins;
        public int Coins
        {
            get {
                return coins;
            }
            set {
                coins = value;
                CoinsChanged?.Invoke();
            }
        }

        private int distance;
        public int Distance
        {
            get {
                return distance;
            }
            set {
                distance = value;
                DistanceChanged?.Invoke();
            }
        }

        private float currentDistance;
        public float CurrentDistance
        {
            get {
                return currentDistance;
            }
            set {
                currentDistance = value;
                CurrentDistanceChanged?.Invoke();
            }
        }

        private float currentSpeedModificator;
        public float CurrentSpeedModificator
        {
            get {
                return currentSpeedModificator;
            }
            set {
                currentSpeedModificator = value;
                CurrentSpeedModificatorChanged?.Invoke();
            }
        }

        private PlayerData()
        {
            if (HasSaved()) {
                Load();
            }
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        public void Save()
        {
            PlayerPrefs.SetString("PlayerData", ToJSONObject().ToString());
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
            CurrentDistance = 0f;
        }

        public void Reset()
        {
            PlayerName = "";
            Coins = 0;
            CurrentCoins = 0;
            Distance = 0;
            CurrentDistance = 0f;
            CurrentSpeedModificator = 0f;

            Save();
        }

        private void ResetCurrentFields()
        {
            CurrentCoins = 0;
            CurrentDistance = 0f;
            CurrentSpeedModificator = 0f;
        }

        private JSONNode ToJSONObject()
        {
            var result = new JSONObject();
            result.Add("PlayerName", PlayerName);
            result.Add("Coins", Coins);
            Distance += (int)CurrentDistance;
            result.Add("Distance", Distance);
            return result;
        }

        public void UpCoins()
        {
            Coins++;
            CurrentCoins++;
        }

        private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            RebindCharacterBodyCollision(newScene);
            ResetCurrentFields();
        }

        private void RebindCharacterBodyCollision(Scene newScene)
        {
            if (newScene.name == "Road") {
                CharacterBodyCollision.Instance.CollisionCoin += OnCharacterBodyCollisionCoin;
                CharacterBodyCollision.Instance.Destroying += () => {
                    CharacterBodyCollision.Instance.CollisionCoin -= OnCharacterBodyCollisionCoin;
                };
            }
        }

        private void OnCharacterBodyCollisionCoin()
        {
            UpCoins();
        }
    }
}
