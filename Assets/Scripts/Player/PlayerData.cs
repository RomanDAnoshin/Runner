using Character;
using Game;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities.SimpleJSON;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private GameObject Character;

        public Action<string> PlayerNameChanged;
        public Action<int> CoinsChanged;
        public Action<int> CurrentCoinsChanged;
        public Action<int> DistanceChanged;
        public Action<float> CurrentDistanceChanged;
        public Action<float> CurrentSpeedModificatorChanged;

        private CharacterBodyCollision characterBodyCollision;

        private string playerName;
        public string PlayerName
        {
            get {
                return playerName;
            }
            set {
                playerName = value;
                PlayerNameChanged?.Invoke(value);
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
                CurrentCoinsChanged?.Invoke(value);
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
                CoinsChanged?.Invoke(value);
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
                DistanceChanged?.Invoke(value);
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
                CurrentDistanceChanged?.Invoke(value);
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
                CurrentSpeedModificatorChanged?.Invoke(value);
            }
        }

        void Awake()
        {
            if (HasSaved()) {
                Load();
            }
            BindCharacterBodyCollision();
            ResetCurrentFields();
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

        private void BindCharacterBodyCollision()
        {
            if (Character != null) {
                characterBodyCollision = Character.GetComponentInChildren<CharacterBodyCollision>();
                characterBodyCollision.CollisionCoin += OnCharacterBodyCollisionCoin;
            }
        }

        private void OnCharacterBodyCollisionCoin()
        {
            UpCoins();
        }

        void OnDestroy()
        {
            if(characterBodyCollision != null) {
                characterBodyCollision.CollisionCoin -= OnCharacterBodyCollisionCoin;
            }
        }
    }
}
