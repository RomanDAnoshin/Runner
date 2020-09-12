using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator Animator;
    public PlayerData PlayerData;
    public CharacterData CharacterData;

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
