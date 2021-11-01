using Character;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities.SimpleJSON;

namespace Player
{
    public class PlayerData // TODO Reset CurrentCoins and CurrentDistance on Game Restart
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

        [SerializeField] private int currentCoins;
        [HideInInspector] public int CurrentCoins
        {
            get {
                return currentCoins;
            }
            set {
                currentCoins = value;
                CurrentCoinsChanged?.Invoke();
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

        [SerializeField] private float currentDistance;
        [HideInInspector] public float CurrentDistance
        {
            get {
                return currentDistance;
            }
            set {
                currentDistance = value;
                CurrentDistanceChanged?.Invoke();
            }
        }

        private PlayerData()
        {
            if (HasSaved()) {
                Load();
            }
            SceneManager.activeSceneChanged += RebindOnCharacterBodyCollisionCoin;
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
            Distance = 0;
            CurrentDistance = 0f;

            Save();
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

        private void RebindOnCharacterBodyCollisionCoin(Scene oldScene, Scene newScene)
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
