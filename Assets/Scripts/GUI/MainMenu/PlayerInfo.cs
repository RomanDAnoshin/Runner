using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.MainMenu
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private GameObject CreateAccountWindow;

        [SerializeField] private Text PlayerName;
        [SerializeField] private Text Coins;
        [SerializeField] private Text Distance;

        void Start()
        {
            if (!PlayerData.HasSaved()) {
                OpenCreateAccountWindow();
            } else {
                PlayerData.Load();
                Refresh();
            }
        }

        public void Refresh()
        {
            Debug.Log("Refresh PlayerName: " + PlayerData.PlayerName);
            PlayerName.text = "Player name: " + PlayerData.PlayerName;
            Coins.text = "Coins: " + PlayerData.Coins.ToString();
            Distance.text = "Distance: " + PlayerData.Distance.ToString();
        }

        public void OnClickButtonReset()
        {
            PlayerData.Reset();
            OpenCreateAccountWindow();
        }

        private void OpenCreateAccountWindow()
        {
            Instantiate(CreateAccountWindow, transform);
        }
    }
}
