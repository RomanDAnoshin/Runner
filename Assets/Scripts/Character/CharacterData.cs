using Road;
using UnityEngine;

namespace Character
{
    public class CharacterData : MonoBehaviour
    {
        public enum CharacterState
        {
            Alive,
            Died
        }

        public CharacterState State { get; protected set; }

        void Start()
        {
            GameData.Instance.StatusChanged += OnGameStatusChanged;
        }

        private void OnGameStatusChanged(GameStatus gameStatus)
        {
            if(gameStatus == GameStatus.Lose) {
                State = CharacterState.Died;
            }
        }

        void OnDestroy()
        {
            GameData.Instance.StatusChanged -= OnGameStatusChanged;
        }
    }
}
