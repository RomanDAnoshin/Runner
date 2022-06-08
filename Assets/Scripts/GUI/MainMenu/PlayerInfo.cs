using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.MainMenu
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Button ButtonReset;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private GameObject CreateAccountWindow;

        [SerializeField] private Text PlayerName;
        [SerializeField] private Text Coins;
        [SerializeField] private Text Distance;

        void Start()
        {
            PlayerData.PlayerNameChanged += OnPlayerNameChanged;
            PlayerData.CoinsChanged += OnCoinsChanged;
            PlayerData.DistanceChanged += OnDistanceChanged;
            ButtonReset.onClick.AddListener(OnClickButtonReset);

            if (!PlayerData.HasSaved()) {
                OpenCreateAccountWindow();
            } else {
                Refresh();
            }
        }

        public void Refresh()
        {
            Distance.text = "Distance: " + PlayerData.Distance.ToString();
            Coins.text = "Coins: " + PlayerData.Coins.ToString();
            PlayerName.text = "Player name: " + PlayerData.PlayerName;
        }

        private void OnDistanceChanged(int value)
        {
            Distance.text = "Distance: " + value.ToString();
        }

        private void OnCoinsChanged(int value)
        {
            Coins.text = "Coins: " + value.ToString();
        }

        private void OnPlayerNameChanged(string value)
        {
            PlayerName.text = "Player name: " + value;
        }

        private void OnClickButtonReset()
        {
            PlayerData.Reset();
            OpenCreateAccountWindow();
        }

        private void OpenCreateAccountWindow()
        {
            var window = Instantiate(CreateAccountWindow, transform);
            var windowSkript = window.GetComponent<CreateAccountWindow>();
            windowSkript.PlayerData = PlayerData;

        }

        void OnDestroy()
        {
            PlayerData.PlayerNameChanged -= OnPlayerNameChanged;
            PlayerData.CoinsChanged -= OnCoinsChanged;
            PlayerData.DistanceChanged -= OnDistanceChanged;
            ButtonReset.onClick.RemoveListener(OnClickButtonReset);
            ButtonReset = null;
            CreateAccountWindow = null;
            PlayerName = null;
            Coins = null;
            Distance = null;
        }
    }
}
