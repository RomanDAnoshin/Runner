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
            CharacterBodyCollision.Instance.CollisionBarricade += OnCharacterCollisionBarricade;
        }

        private void OnCharacterCollisionBarricade()
        {
            State = CharacterState.Died;
        }

        void OnDestroy()
        {
            CharacterBodyCollision.Instance.CollisionBarricade -= OnCharacterCollisionBarricade;
        }
    }
}
