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
        [SerializeField] private Button ButtonRestart;
        [SerializeField] private Button ButtonQuit;

        void Start()
        {
            ButtonRestart.onClick.AddListener(OnClickButtonRestart);
            ButtonQuit.onClick.AddListener(OnClickButtonQuit);
        }

        private void OnClickButtonRestart()
        {
            PlayerData.Instance.Save();
            SceneManager.LoadScene("Road");
        }

        private void OnClickButtonQuit()
        {
            PlayerData.Instance.Save();
            SceneManager.LoadScene("MainMenu");
        }

        void OnDestroy()
        {
            ButtonRestart.onClick.RemoveListener(OnClickButtonRestart);
            ButtonRestart = null;
            ButtonQuit.onClick.RemoveListener(OnClickButtonQuit);
            ButtonQuit = null;
        }
    }
}
