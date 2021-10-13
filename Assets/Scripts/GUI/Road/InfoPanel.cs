using Player;
using Road;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Road
{
    public class InfoPanel : MonoBehaviour
    {
        private PlayerData playerData;
        private RoadMovement roadMovement;
        [SerializeField] private Text Coins;
        [SerializeField] private Text Distance;
        [SerializeField] private Text SpeedModificator;

        void Start()
        {
            playerData = FindObjectOfType<PlayerData>();
            playerData.CoinsChanged += OnCoinsCountChanged;
            playerData.CurrentDistanceChanged += OnCurrentDistanceCountChanged;
            roadMovement = FindObjectOfType<RoadMovement>();
            roadMovement.SpeedModificatorChanged += OnSpeedModificatorChanged;
        }

        private void OnCoinsCountChanged()
        {
            Coins.text = "Total coins: " + playerData.Coins.ToString();
        }

        private void OnCurrentDistanceCountChanged()
        {
            Distance.text = "Current distance: " + ((int)playerData.CurrentDistance).ToString();
        }

        private void OnSpeedModificatorChanged()
        {
            var speedModstring = roadMovement.SpeedModificator.ToString();
            if (speedModstring.Length >= 3) {
                SpeedModificator.text = "Speed Modificator: x" + speedModstring.Substring(0, 3);
            } else {
                SpeedModificator.text = "Speed Modificator: x" + speedModstring;
            }
        }

        void OnDestroy()
        {
            playerData.CoinsChanged -= OnCoinsCountChanged;
            playerData.CurrentDistanceChanged -= OnCurrentDistanceCountChanged;
            playerData = null;
            roadMovement.SpeedModificatorChanged -= OnSpeedModificatorChanged;
            roadMovement = null;
            Coins = null;
            Distance = null;
            SpeedModificator = null;
        }
    }
}
