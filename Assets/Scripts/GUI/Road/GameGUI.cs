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

        void Start()
        {
            PlayerInput.Instance.Ran += OnPlayerRan;
            GameData.Instance.Lost += OnGameLost;
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
            Instantiate(DeathWindow, transform);
        }

        void OnDestroy()
        {
            PlayerInput.Instance.Ran -= OnPlayerRan;
            GameData.Instance.Lost -= OnGameLost;
            StartMessage = null;
            DeathWindow = null;
        }
    }
}
