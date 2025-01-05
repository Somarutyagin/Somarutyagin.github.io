using UnityEngine;

[RequireComponent(typeof(PlayerConfig))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private JoystickMovement _movement;
    [SerializeField] private JoystickAttack _attack;
    private PlayerConfig _config;

    private Transform _weapon;
    private Transform _camera;
    private Vector3 _cameraPosStart;
    private Vector3 _playerPos;

    private readonly float _rotationSpeed = 2f;

    private Vector3 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    public void Initialize()
    {
        _camera = Camera.main.transform;
        _camera.position = _cameraPosStart;
        _weapon = transform.GetChild(1);
        _config = gameObject.GetComponent<PlayerConfig>();
    }

    private void Update()
    {
        if (GameManager.Instance.activeGameStatus == GameStatus.Play)
        {
            if (Position.x < GameManager.Instance.Border && Position.x > -1 * GameManager.Instance.Border && Position.y < GameManager.Instance.Border && Position.y > -1 * GameManager.Instance.Border)
            {
                _playerPos = Position;

                Vector3 Direction = _movement.MoveDirection();
                transform.Translate(Direction.x * _config.Speed * Time.deltaTime, Direction.y * _config.Speed * Time.deltaTime, 0);
            }
            else
            {
                Position = _playerPos;
            }
            
            _weapon.transform.rotation = Quaternion.Slerp(_weapon.transform.rotation, _attack.AttackDirection(_weapon.transform.position), _rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(_weapon.transform.rotation, _attack.AttackDirection(_weapon.transform.position)) < 0.1f)
            {
                _weapon.transform.rotation = _attack.AttackDirection(_weapon.transform.position);
            }
        }

        _camera.position = new Vector3(_cameraPosStart.x + transform.position.x, _cameraPosStart.y + transform.position.y, -10);
    }
}
