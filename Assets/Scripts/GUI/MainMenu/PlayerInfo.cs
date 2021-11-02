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
        [SerializeField] private GameObject CreateAccountWindow;

        [SerializeField] private Text PlayerName;
        [SerializeField] private Text Coins;
        [SerializeField] private Text Distance;

        void Start()
        {
            PlayerData.Instance.PlayerNameChanged += OnPlayerNameChanged;
            PlayerData.Instance.CoinsChanged += OnCoinsChanged;
            PlayerData.Instance.DistanceChanged += OnDistanceChanged;
            ButtonReset.onClick.AddListener(OnClickButtonReset);

            if (!PlayerData.Instance.HasSaved()) {
                OpenCreateAccountWindow();
            } else {
                Refresh();
            }
        }

        public void Refresh()
        {
            Distance.text = "Distance: " + PlayerData.Instance.Distance.ToString();
            Coins.text = "Coins: " + PlayerData.Instance.Coins.ToString();
            PlayerName.text = "Player name: " + PlayerData.Instance.PlayerName;
        }

        private void OnDistanceChanged()
        {
            Distance.text = "Distance: " + PlayerData.Instance.Distance.ToString();
        }

        private void OnCoinsChanged()
        {
            Coins.text = "Coins: " + PlayerData.Instance.Coins.ToString();
        }

        private void OnPlayerNameChanged()
        {
            PlayerName.text = "Player name: " + PlayerData.Instance.PlayerName;
        }

        private void OnClickButtonReset()
        {
            PlayerData.Instance.Reset();
            OpenCreateAccountWindow();
        }

        private void OpenCreateAccountWindow()
        {
            Instantiate(CreateAccountWindow, transform);
        }

        void OnDestroy()
        {
            PlayerData.Instance.PlayerNameChanged -= OnPlayerNameChanged;
            PlayerData.Instance.CoinsChanged -= OnCoinsChanged;
            PlayerData.Instance.DistanceChanged -= OnDistanceChanged;
            ButtonReset.onClick.RemoveListener(OnClickButtonReset);
            ButtonReset = null;
            CreateAccountWindow = null;
            PlayerName = null;
            Coins = null;
            Distance = null;
        }
    }
}
