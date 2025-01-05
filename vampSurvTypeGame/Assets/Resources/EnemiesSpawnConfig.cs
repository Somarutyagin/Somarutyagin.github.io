using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemiesSpawnConfig", fileName = "EnemiesSpawnConfig")]
public class EnemiesSpawnConfig : ScriptableObject
{
    [field: SerializeField] public GameObject Enemy { get; private set; }
}
