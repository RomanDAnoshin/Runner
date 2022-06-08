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
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private GameData GameData;

        void Start()
        {
            PlayerInput.Ran += OnPlayerRan;
            GameData.Lost += OnGameLost;
        }

        private void OnPlayerRan()
        {
            Destroy(StartMessage);
        }

        private void OnGameLost()
        {
            StartCoroutine("OpenDeathWindow");
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
            PlayerInput.Ran -= OnPlayerRan;
            GameData.Lost -= OnGameLost;
            StartMessage = null;
            DeathWindow = null;
        }
    }
}
