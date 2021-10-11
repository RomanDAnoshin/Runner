using Player;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Road
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private Text Coins;
        [SerializeField] private Text Distance;

        void Update()
        {
            Coins.text = "Total coins: " + PlayerData.Coins.ToString();
            Distance.text = "Current distance: " + PlayerData.CurrentDistance.ToString();
        }
    }
}
