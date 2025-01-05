using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    [SerializeField] private TextMeshPro _hpTxt;

    private const float _damageDefault = 20.0f;
    private const float _speedDefault = 4.0f;
    private const float _hpDefault = 100.0f;

    [HideInInspector] public float Damage { get; private set; }
    [HideInInspector] public float Speed { get; private set; }
    [HideInInspector] public float Hp { get; private set; }
    [HideInInspector] public float MaxHp { get; private set; }

    [HideInInspector] public float HpScaler = 1f;
    [HideInInspector] public float SpeedScaler = 1f;
    [HideInInspector] public float DamageScaler = 1f;

    private bool _isDamageTakeCooldown;

    public void Initialize()
    {
        Reset();
    }

    public void TakeDamage(float value)
    {
        if (!_isDamageTakeCooldown)
        {
            StartCoroutine(DamageTakeCooldown());

            Hp -= value;

            if (Hp < 0)
                Hp = 0;
        }
    }
    public void FullHeal()
    {
        Hp = MaxHp;
    }
    private IEnumerator DamageTakeCooldown()
    {
        _isDamageTakeCooldown = true;
        yield return new WaitForSeconds(1f);

        _isDamageTakeCooldown = false;
    }
    private void Update()
    {
        MaxHp = HpScaler * _hpDefault;
        Speed = _speedDefault * SpeedScaler;
        Damage = _damageDefault * DamageScaler;

        if (Hp > MaxHp)
            Hp = MaxHp;

        if (Hp <= 0)
            GameManager.Instance.Lose();

        if (_hpTxt != null)
        {
            _hpTxt.text = Hp.ToString();

            _hpTxt.color = new Color(1 - (Hp / _hpDefault), Hp / _hpDefault, 0, 1);
        }
    }
    public void Reset()
    {
        Damage = _damageDefault;
        Speed = _speedDefault;
        Hp = _hpDefault;
    }
}
