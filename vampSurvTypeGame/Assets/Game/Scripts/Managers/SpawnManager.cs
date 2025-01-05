using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("SpawnManager").AddComponent<SpawnManager>();
            }
            return _instance;
        }
    }
    private GameObject _enemy;
    private GameObject _enemyPool;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Initialize()
    {
        _enemyPool = GameObject.Find("Enemy Pool");
        _enemy = Resources.Load<GameObject>("Prefabs/Enemy");
    }
    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }
    public void StopSpawn()
    {
        StopAllCoroutines();
    }
    public void Reset()
    {
        for (int i = 0; i < _enemyPool.transform.childCount; i++)
        {
            Destroy(_enemyPool.transform.GetChild(i).gameObject);
        }
    }
    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            while (_enemyPool.transform.childCount != 0) { yield return null; }

            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-1 * GameManager.Instance.Border, GameManager.Instance.Border + 1), Random.Range(-1 * GameManager.Instance.Border, GameManager.Instance.Border + 1));
        Instantiate(_enemy, spawnPos, Quaternion.identity, _enemyPool.transform);
    }
}
