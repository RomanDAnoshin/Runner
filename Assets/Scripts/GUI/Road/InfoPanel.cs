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

        void Start()
        {
            PlayerData.Instance.CoinsChanged += OnCoinsCountChanged;
            PlayerData.Instance.CurrentDistanceChanged += OnCurrentDistanceCountChanged;
            PlayerData.Instance.CurrentSpeedModificatorChanged += OnSpeedModificatorChanged;
            Refresh();
        }

        private void OnCoinsCountChanged()
        {
            Coins.text = "Total coins: " + PlayerData.Instance.Coins.ToString();
        }

        private void OnCurrentDistanceCountChanged()
        {
            Distance.text = "Current distance: " + ((int)PlayerData.Instance.CurrentDistance).ToString();
        }

        private void OnSpeedModificatorChanged()
        {
            var speedModstring = PlayerData.Instance.CurrentSpeedModificator.ToString();
            if (speedModstring.Length >= 3) {
                SpeedModificator.text = "Speed Modificator: x" + speedModstring.Substring(0, 3);
            } else {
                SpeedModificator.text = "Speed Modificator: x" + speedModstring;
            }
        }

        private void Refresh()
        {
            OnCoinsCountChanged();
            OnCurrentDistanceCountChanged();
        }

        void OnDestroy()
        {
            PlayerData.Instance.CoinsChanged -= OnCoinsCountChanged;
            PlayerData.Instance.CurrentDistanceChanged -= OnCurrentDistanceCountChanged;
            PlayerData.Instance.CurrentSpeedModificatorChanged -= OnSpeedModificatorChanged;
            Coins = null;
            Distance = null;
            SpeedModificator = null;
        }
    }
}
