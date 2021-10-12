using Player;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Road
{
    public class InfoPanel : MonoBehaviour
    {
        private PlayerData playerData;
        [SerializeField] private Text Coins;
        [SerializeField] private Text Distance;

        void Start()
        {
            playerData = FindObjectOfType<PlayerData>();
            playerData.CoinsChanged += OnCoinsCountChanged;
            playerData.CurrentDistanceChanged += OnCurrentDistanceCountChanged;
        }

        private void OnCoinsCountChanged()
        {
            Coins.text = "Total coins: " + playerData.Coins.ToString();
        }

        private void OnCurrentDistanceCountChanged()
        {
            Distance.text = "Current distance: " + playerData.CurrentDistance.ToString();
        }
    }
}
