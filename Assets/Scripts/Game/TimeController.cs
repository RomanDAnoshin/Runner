using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private GameData GameData;

        public bool IsPause { get; protected set; } = false;

        void Start()
        {
            if (GameData != null) {
                GameData.Paused += Pause; // TODO: Refactoring, This should be controlled through a new class GameController
                GameData.Unpaused += Unpause;
            }
        }

        public void Pause()
        {
            IsPause = true;
            Time.timeScale = 0.0f;
        }

        public void Unpause()
        {
            IsPause = false;
            Time.timeScale = 1.0f;
        }

        void OnDestroy()
        {
            Unpause(); // TODO: Refactoring
            if (GameData != null) {
                GameData.Paused -= Pause;
                GameData.Unpaused -= Unpause;
            }
        }
    }
}
