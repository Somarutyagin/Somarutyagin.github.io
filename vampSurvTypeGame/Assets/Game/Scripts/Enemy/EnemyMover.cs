using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private Transform _player;
    private readonly float _speed = 2f;
    public bool IsKick = false;

    void Start()
    {
        _player = GameObject.Find("Player")?.transform;
    }

    void Update()
    {
        if (_player != null && !IsKick)
            transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }
}
