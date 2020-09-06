using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public float SpeedVertical;
    public float SpeedHorizontal;

    public float CurrentSpeedVertical;

    public Vector3 VelocitySmoothDamp = Vector3.zero;
}
