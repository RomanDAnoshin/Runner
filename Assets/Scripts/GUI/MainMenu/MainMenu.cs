using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GUI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public void OnClickButtonStartGame()
        {
            SceneManager.LoadScene("Road");
        }

        public void OnClickButtonQuit()
        {
            Application.Quit();
        }
    }
}
