using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class EnemyConfig : MonoBehaviour
{
    private EnemyMover _enemyMover;
    [SerializeField] private TextMeshPro _hpTxt;

    private const float _damageDefault = 20.0f;
    private const float _speedDefault = 2.0f;
    private const float _hpDefault = 100.0f;

    [HideInInspector] public float Damage { get; private set; }
    [HideInInspector] public float Speed { get; private set; }
    [HideInInspector] public float Hp { get; private set; }

    private void Awake()
    {
        Hp = _hpDefault;
        Damage = _damageDefault;
        Speed = _speedDefault;

        _enemyMover = GetComponent<EnemyMover>();
    }
    private void Update()
    {
        _hpTxt.text = Hp.ToString();

        _hpTxt.color = new Color(1 - (Hp / _hpDefault), Hp / _hpDefault, 0, 1);

        if (Hp <= 0)
        {
            Kill();
        }
    }
    public void DealDamage(float value, Vector2 damagePoint)
    {
        Hp -= value;

        if (Hp < 0)
            Hp = 0;

        //StartCoroutine(KickBack(damagePoint));
    }
    private IEnumerator KickBack(Vector2 damagePoint)
    {
        _enemyMover.IsKick = true;

        int iterations = 100;
        float time = 0.2f;
        float pushPower = 0.05f;
        Vector3 distance = transform.position - (Vector3)damagePoint;

        for (int i = 0; i < iterations; i++)
        {
            yield return new WaitForSeconds(time / iterations);

            transform.position = transform.position + new Vector3(distance.x / iterations * pushPower / (1f / i), distance.y / iterations * pushPower / (1f / i), 0);
        }

        _enemyMover.IsKick = false;
    }
    private void Kill()
    {
        GameManager.Instance.Score++;
        Destroy(gameObject);
    }
}
