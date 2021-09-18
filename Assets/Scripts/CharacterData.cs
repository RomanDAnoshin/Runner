using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public enum CharacterState
    {
        Alive,
        Died
    }

    public CharacterState State;

    public float SpeedVertical;
    public float SpeedHorizontal;
    [Range(0f, 1f), Header("CurrentSpeedVertical = SpeedVertical + PlayerData.Coins * Modificator")]
    public float SpeedVerticalModificator;
}
