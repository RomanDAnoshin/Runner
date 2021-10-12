using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.MainMenu
{
    public class PlayerInfo : MonoBehaviour
    {
        private PlayerData playerData;
        [SerializeField] private Button ButtonReset;
        [SerializeField] private GameObject CreateAccountWindow;

        [SerializeField] private Text PlayerName;
        [SerializeField] private Text Coins;
        [SerializeField] private Text Distance;

        void Start()
        {
            playerData = FindObjectOfType<PlayerData>();
            playerData.PlayerNameChanged += OnPlayerNameChanged;
            playerData.CoinsChanged += OnCoinsChanged;
            playerData.DistanceChanged += OnDistanceChanged;
            ButtonReset.onClick.AddListener(OnClickButtonReset);

            if (!playerData.HasSaved()) {
                OpenCreateAccountWindow();
            } else {
                Refresh();
            }
        }

        public void Refresh()
        {
            Distance.text = "Distance: " + playerData.Distance.ToString();
            Coins.text = "Coins: " + playerData.Coins.ToString();
            PlayerName.text = "Player name: " + playerData.PlayerName;
        }

        private void OnDistanceChanged()
        {
            Distance.text = "Distance: " + playerData.Distance.ToString();
        }

        private void OnCoinsChanged()
        {
            Coins.text = "Coins: " + playerData.Coins.ToString();
        }

        private void OnPlayerNameChanged()
        {
            PlayerName.text = "Player name: " + playerData.PlayerName;
        }

        private void OnClickButtonReset()
        {
            playerData.Reset();
            OpenCreateAccountWindow();
        }

        private void OpenCreateAccountWindow()
        {
            Instantiate(CreateAccountWindow, transform);
        }
    }
}
