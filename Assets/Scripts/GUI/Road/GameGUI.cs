using Game;
using Player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Road
{
    public class GameGUI : MonoBehaviour
    {
        public Action ButtonUnpauseClicked;

        [SerializeField] private GameObject StartMessage;
        [SerializeField] private GameObject DeathWindow;
        [SerializeField] private GameObject PauseWindow;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private GameData GameData;

        [SerializeField] private Button ButtonPause;

        private PauseWindow pauseWindowScript; // TODO: Refactoring

        void Start()
        {
            GameData.Played += OnGamePlayed;
            GameData.Lost += OnGameLost;
            ButtonPause.onClick.AddListener(OnClickButtonPause);
            ButtonPause.interactable = false;
        }

        private void OnGamePlayed()
        {
            Destroy(StartMessage);
            ButtonPause.interactable = true;
        }

        private void OnGameLost()
        {
            ButtonPause.interactable = false;
            StartCoroutine(OpenDeathWindow());
        }

        private IEnumerator OpenDeathWindow()
        {
            yield return new WaitForSeconds(4);
            var window = Instantiate(DeathWindow, transform);
            var windowScript = window.GetComponent<DeathWindow>();
            windowScript.PlayerData = PlayerData;
        }

        private void OnClickButtonPause()
        {
            ButtonPause.interactable = false;
            OpenPauseWindow();
        }

        private void OpenPauseWindow()
        {
            var window = Instantiate(PauseWindow, transform);
            pauseWindowScript = window.GetComponent<PauseWindow>();
            pauseWindowScript.PlayerData = PlayerData;
            pauseWindowScript.ButtonUnpauseClicked += OnClickButtonUnpause;
        }

        private void OnClickButtonUnpause()
        {
            pauseWindowScript.ButtonUnpauseClicked -= OnClickButtonUnpause;
            ButtonUnpauseClicked?.Invoke();
            ButtonPause.interactable = true;
        }

        void OnDestroy()
        {
            GameData.Played -= OnGamePlayed;
            GameData.Lost -= OnGameLost;
            ButtonPause.onClick.RemoveListener(OnClickButtonPause);
        }
    }
}
