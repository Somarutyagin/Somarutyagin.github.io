using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerCollision _playerCollision;
    [SerializeField] private JoystickMovement _joystickMovement;
    [SerializeField] private JoystickAttack _joystickAttack;

    void Awake()
    {
        AudioManager.Instance.Initialize();
        SpawnManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        GameManager.Instance.Initialize();

        _joystickMovement.Initialize();
        _joystickAttack.Initialize();
        _playerConfig.Initialize();
        _playerCollision.Initialize();
        _playerMover.Initialize();
    }
}
