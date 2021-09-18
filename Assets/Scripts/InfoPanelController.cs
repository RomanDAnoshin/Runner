using UnityEngine;
using UnityEngine.UI;

public class InfoPanelController : MonoBehaviour
{
    public PlayerData PlayerData;
    public Text Coins;
    public Text Distance;
    public CoinUIAnimation CoinUIAnimation;

    void Update()
    {
        Coins.text = "Total coins: " + PlayerData.Coins.ToString();
        Distance.text = "Current distance: " + PlayerData.CurrentDistance.ToString();
    }

    public void StartCoinAnimation()
    {
        CoinUIAnimation.StartAnimation();
    }
}
