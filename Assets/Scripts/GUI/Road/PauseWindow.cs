using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GUI.Road
{
    public class PauseWindow : MonoBehaviour
    {
        public Action ButtonUnpauseClicked;

        [SerializeField] private Button ButtonUnpause;
        [SerializeField] private Button ButtonQuit;

        public PlayerData PlayerData;

        void Start()
        {
            ButtonUnpause.onClick.AddListener(OnClickButtonUnpause);
            ButtonQuit.onClick.AddListener(OnClickButtonQuit);
        }

        private void OnClickButtonUnpause()
        {
            ButtonUnpauseClicked?.Invoke();
            Destroy(gameObject); // TODO: Refactoring
        }

        private void OnClickButtonQuit()
        {
            PlayerData.Save();
            SceneManager.LoadScene("MainMenu");
        }

        void OnDestroy()
        {
            ButtonUnpause.onClick.RemoveListener(OnClickButtonUnpause);
            ButtonQuit.onClick.RemoveListener(OnClickButtonQuit);
        }
    }
}
