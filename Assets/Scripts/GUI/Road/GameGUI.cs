using Player;
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

        void Start()
        {
            PlayerData.Load();
        }

        public void OnPlayerActed()
        {
            if (PlayerInput.Value == PlayerInput.PlayerActions.Run) {
                Destroy(StartMessage);
            }
        }

        public void OnCharacterDied()
        {
            StartCoroutine("OpenDeathWindow");
        }

        private IEnumerator OpenDeathWindow()
        {
            yield return new WaitForSeconds(4);
            Instantiate(DeathWindow, transform);
        }
    }
}
