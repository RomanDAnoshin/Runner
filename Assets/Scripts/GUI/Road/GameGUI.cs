using Character;
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

        void Start()
        {
            PlayerInput.Instance.PlayerActed += OnPlayerActed;
            CharacterBodyCollision.Instance.CollisionBarricade += OnCharacterCollisionBarricade;
        }

        private void OnPlayerActed()
        {
            if (PlayerInput.Instance.Value == PlayerInput.PlayerActions.Run) {
                Destroy(StartMessage);
            }
        }

        private void OnCharacterCollisionBarricade()
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
            PlayerInput.Instance.PlayerActed -= OnPlayerActed;
            CharacterBodyCollision.Instance.CollisionBarricade -= OnCharacterCollisionBarricade;
            StartMessage = null;
            DeathWindow = null;
        }
    }
}
