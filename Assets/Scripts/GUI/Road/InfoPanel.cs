using Player;
using Road;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Road
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private Text Coins;
        [SerializeField] private Text Distance;
        [SerializeField] private Text SpeedModificator;

        [SerializeField] private PlayerData PlayerData;

        void Start()
        {
            PlayerData.CoinsChanged += OnCoinsCountChanged;
            PlayerData.CurrentDistanceChanged += OnCurrentDistanceCountChanged;
            PlayerData.CurrentSpeedModificatorChanged += OnSpeedModificatorChanged;
            Refresh();
        }

        private void OnCoinsCountChanged(int value)
        {
            Coins.text = "Total coins: " + value.ToString();
        }

        private void OnCurrentDistanceCountChanged(float value)
        {
            Distance.text = "Current distance: " + ((int)value).ToString();
        }

        private void OnSpeedModificatorChanged(float value)
        {
            var speedModstring = value.ToString();
            if (speedModstring.Length >= 3) {
                SpeedModificator.text = "Speed Modificator: x" + speedModstring.Substring(0, 3);
            } else {
                SpeedModificator.text = "Speed Modificator: x" + speedModstring;
            }
        }

        private void Refresh()
        {
            OnCoinsCountChanged(PlayerData.Coins);
            OnCurrentDistanceCountChanged(PlayerData.CurrentDistance);
        }

        void OnDestroy()
        {
            PlayerData.CoinsChanged -= OnCoinsCountChanged;
            PlayerData.CurrentDistanceChanged -= OnCurrentDistanceCountChanged;
            PlayerData.CurrentSpeedModificatorChanged -= OnSpeedModificatorChanged;
            Coins = null;
            Distance = null;
            SpeedModificator = null;
        }
    }
}
