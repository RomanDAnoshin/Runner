using Character;
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

        void Start()
        {
            PlayerInput.Instance.PlayerActed += OnPlayerActed;
            GameData.Instance.StatusChanged += OnGameStatusChanged;
        }

        private void OnPlayerActed()
        {
            if (PlayerInput.Instance.Value == PlayerInput.PlayerActions.Run) {
                Destroy(StartMessage);
            }
        }

        private void OnGameStatusChanged(GameStatus gameStatus)
        {
            if(gameStatus == GameStatus.Lose) {
                StartCoroutine("OpenDeathWindow");
            }
        }

        private IEnumerator OpenDeathWindow()
        {
            yield return new WaitForSeconds(4);
            Instantiate(DeathWindow, transform);
        }

        void OnDestroy()
        {
            PlayerInput.Instance.PlayerActed -= OnPlayerActed;
            GameData.Instance.StatusChanged -= OnGameStatusChanged;
            StartMessage = null;
            DeathWindow = null;
        }
    }
}
