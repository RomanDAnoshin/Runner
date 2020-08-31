using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // TODO: output PlayerData, ButtonResetPlayerData, DropdownSelectPlayer

    public void OnClickButtonStartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickButtonQuit()
    {
        Application.Quit();
    }
}
