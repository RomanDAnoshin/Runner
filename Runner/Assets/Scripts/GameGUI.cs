using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    public Text PlayerInfoPanel;
    public GameObject GameMessage;
    public GameObject DeathWindow;

    public PlayerData PlayerData;
    public PlayerInput PlayerInput;

    void Start()
    {
        PlayerData.Load();
    }

    void Update()
    {
        PlayerInfoPanel.text = 
            "Total coins: " + PlayerData.Coins.ToString() + "\n" +
            "Current distance: " + PlayerData.CurrentDistance.ToString();

        // TODO: subscribe on event
        if(PlayerInput.Value == PlayerInput.InputType.Run) {
            GameMessage.SetActive(false);
        }
    }

    public void OnClickButtonRestart()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickButtonQuit()
    {
        PlayerData.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnDeath()
    {
        DeathWindow.SetActive(true);
    }
}
