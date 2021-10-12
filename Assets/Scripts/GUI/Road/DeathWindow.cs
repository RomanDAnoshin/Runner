using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GUI.Road
{
    public class DeathWindow : MonoBehaviour
    {
        private PlayerData playerData;
        [SerializeField] private Button ButtonRestart;
        [SerializeField] private Button ButtonQuit;

        void Start()
        {
            playerData = FindObjectOfType<PlayerData>();
            ButtonRestart.onClick.AddListener(OnClickButtonRestart);
            ButtonQuit.onClick.AddListener(OnClickButtonQuit);
        }

        private void OnClickButtonRestart()
        {
            playerData.Save();
            SceneManager.LoadScene("Road");
        }

        private void OnClickButtonQuit()
        {
            playerData.Save();
            SceneManager.LoadScene("MainMenu");
        }
    }
}
