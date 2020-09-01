using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public PlayerData PlayerData;
    public GameObject WindowCreateAccount;
    public InputField InputField;

    // info panel
    public Text PlayerName;
    public Text Coins;
    public Text Distance;

    void Start()
    {
        if(!PlayerData.HasSaved()) {
            CreateAccount();
        } else {
            PlayerData.Load();
            RefreshInfoPanel();
        }
    }

    public void OnClickButtonReset()
    {
        PlayerData.Reset();

        CreateAccount();
    }

    public void OnClickButtonStartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickButtonQuit()
    {
        Application.Quit();
    }

    public void OnClickButtonOk()
    {
        RefreshInfoPanel();
        WindowCreateAccount.SetActive(false);
    }

    public void OnEndEditInputField(string value)
    {
        PlayerData.PlayerName = InputField.text;
        PlayerData.Save();
    }

    private void CreateAccount()
    {
        WindowCreateAccount.SetActive(true);
    }

    private void RefreshInfoPanel()
    {
        PlayerName.text = "Player name: " + PlayerData.PlayerName;
        Coins.text = "Coins: " + PlayerData.Coins.ToString();
        Distance.text = "Distance: " + PlayerData.Distance.ToString();
    }
}
