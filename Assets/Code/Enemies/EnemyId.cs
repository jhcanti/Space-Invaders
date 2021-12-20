using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/New enemy")]
public class EnemyId : ScriptableObject
{
    [SerializeField] private string enemyId;

    public string Value => enemyId;

}
