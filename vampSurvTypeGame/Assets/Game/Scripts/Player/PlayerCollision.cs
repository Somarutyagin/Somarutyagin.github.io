using UnityEngine;

[RequireComponent(typeof(PlayerConfig))]
public class PlayerCollision : MonoBehaviour
{
    private PlayerConfig _config;

    public void Initialize()
    {
        _config = GetComponent<PlayerConfig>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _config.TakeDamage(collision.GetComponent<EnemyConfig>().Damage);
        }
    }
}
