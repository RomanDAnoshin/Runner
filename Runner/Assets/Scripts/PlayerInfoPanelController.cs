using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanelController : MonoBehaviour
{
    public PlayerData PlayerData;
    public Text Coins;
    public Text Distance;
    public CoinUIAnimation CoinUIAnimation;

    void Start()
    {
        
    }

    void Update()
    {
        Coins.text = "Total coins: " + PlayerData.Coins.ToString();
        Distance.text = "Current distance: " + ((int)(PlayerData.CurrentDistance)).ToString();
    }

    public void StartCoinAnimation()
    {
        CoinUIAnimation.StartAnimation();
    }
}
