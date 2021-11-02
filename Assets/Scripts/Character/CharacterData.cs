using Game;
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
            GameData.Instance.Lost += OnGameLost;
        }

        private void OnGameLost()
        {
            State = CharacterState.Died;
        }

        void OnDestroy()
        {
            GameData.Instance.Lost -= OnGameLost;
        }
    }
}
