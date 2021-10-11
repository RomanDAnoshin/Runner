using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI.Road
{
    public class DeathWindow : MonoBehaviour
    {
        [SerializeField] private PlayerData PlayerData;

        public void OnClickButtonRestart()
        {
            PlayerData.Save();
            SceneManager.LoadScene("Road");
        }

        public void OnClickButtonQuit()
        {
            PlayerData.Save();
            SceneManager.LoadScene("MainMenu");
        }
    }
}
