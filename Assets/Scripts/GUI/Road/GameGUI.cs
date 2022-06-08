using Character;
using Game;
using Player;
using Road;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI.Road
{
    public class GameGUI : MonoBehaviour
    {
        [SerializeField] private GameObject StartMessage;
        [SerializeField] private GameObject DeathWindow;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private GameData GameData;

        void Start()
        {
            GameData.Played += OnGamePlayed;
            GameData.Lost += OnGameLost;
        }

        private void OnGamePlayed()
        {
            Destroy(StartMessage);
        }

        private void OnGameLost()
        {
            StartCoroutine(OpenDeathWindow());
        }

        private IEnumerator OpenDeathWindow()
        {
            yield return new WaitForSeconds(4);
            var window = Instantiate(DeathWindow, transform);
            var windowScript = window.GetComponent<DeathWindow>();
            windowScript.PlayerData = PlayerData;
        }

        void OnDestroy()
        {
            GameData.Played -= OnGamePlayed;
            GameData.Lost -= OnGameLost;
        }
    }
}
