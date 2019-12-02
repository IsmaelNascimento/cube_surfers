using UnityEngine;

public enum EnemyGroup
{
    Red,
    Blue,
    Yellow
}

[CreateAssetMenu(fileName = "EnemyTypeData", menuName = "CubeSurfers/EnemyTypeData")]
public class EnemyType : ScriptableObject
{
    public EnemyGroup enemyNameGroup;
    public Material materialColor;
}