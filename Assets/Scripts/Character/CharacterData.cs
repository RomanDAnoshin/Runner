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

        public void OnCharacterCollisionBarricade()
        {
            State = CharacterState.Died;
        }
    }
}
