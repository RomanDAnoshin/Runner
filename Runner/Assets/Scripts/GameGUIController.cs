using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameGUIController : MonoBehaviour
{
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
        // TODO: subscribe on event
        if (PlayerInput.Value == PlayerInput.InputType.Run) {
            GameMessage.SetActive(false);
        }
    }

    public void OnClickButtonRestart()
    {
        PlayerData.Save();
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
