using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GUI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button ButtonStartGame;
        [SerializeField] private Button ButtonQuit;

        void Start()
        {
            ButtonStartGame.onClick.AddListener(OnClickButtonStartGame);
            ButtonQuit.onClick.AddListener(OnClickButtonQuit);
        }

        private void OnClickButtonStartGame()
        {
            SceneManager.LoadScene("Road");
        }

        private void OnClickButtonQuit()
        {
            Application.Quit();
        }

        void OnDestroy()
        {
            ButtonStartGame.onClick.RemoveListener(OnClickButtonStartGame);
            ButtonStartGame = null;
            ButtonQuit.onClick.RemoveListener(OnClickButtonQuit);
            ButtonQuit = null;
        }
    }
}
