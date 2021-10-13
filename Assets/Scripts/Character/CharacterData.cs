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

        private CharacterBodyCollision characterBodyCollision;

        void Start()
        {
            characterBodyCollision = FindObjectOfType<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
        }

        private void OnCharacterCollisionBarricade()
        {
            State = CharacterState.Died;
        }

        void OnDestroy()
        {
            characterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
            characterBodyCollision = null;
        }
    }
}
