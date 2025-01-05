using UnityEngine;

[RequireComponent(typeof(EnemyConfig))]
public class EnemyCollision : MonoBehaviour
{
    private EnemyConfig _config;
    private PlayerConfig _playerConfig;

    private void Awake()
    {
        _playerConfig = GameObject.Find("Player").GetComponent<PlayerConfig>();
        _config = gameObject.GetComponent<EnemyConfig>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("weapon"))
        {
            _config.DealDamage(_playerConfig.Damage, other.ClosestPoint(transform.position));
        }
    }
}
