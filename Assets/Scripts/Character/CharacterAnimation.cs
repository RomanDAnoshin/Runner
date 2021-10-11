using UnityEngine;
using Player;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private CharacterData CharacterData;

        public void Run()
        {
            Animator.SetTrigger("Run");
        }

        public void Die()
        {
            Animator.speed = 1f;
            Animator.SetTrigger("Die");
        }

        public void UpdateSpeed()
        {
            Animator.speed = 1f + (PlayerData.Coins * CharacterData.SpeedVerticalModificator) / 100f;
        }
    }
}
