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
            var characterBodyCollision = FindObjectOfType<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
        }

        private void OnCharacterCollisionBarricade()
        {
            State = CharacterState.Died;
        }
    }
}
