using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void OnPlayerActed()
    {
        if (PlayerInput.Value == PlayerInput.PlayerActions.Run) {
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

    public void OnCharacterDied()
    {
        StartCoroutine("OpenDeathWindow");
    }

    private IEnumerator OpenDeathWindow()
    {
        yield return new WaitForSeconds(4);
        DeathWindow.SetActive(true);
    }
}
