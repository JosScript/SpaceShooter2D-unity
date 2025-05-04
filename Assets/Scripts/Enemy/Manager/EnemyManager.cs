using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private BossController bossPrefab;
    [SerializeField] private EnemyController enemyPrefab;
    private float _secondsBetweenSpawn = 2f;
    private bool _stopSpawning;
    private ObjectPool<EnemyController> _enemyPool;
    private PlayerManager _player;
    
    private void Awake()
    {
        _enemyPool = new ObjectPool<EnemyController>(CreateEnemy, OnGetEnemy, OnReturnEnemy);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(WaitPlayerSpawn());
        StartCoroutine(InstantiateEnemies());
    }

    public void SetPlayer(PlayerManager player) => _player = player;
    
    private IEnumerator WaitPlayerSpawn()
    {
        yield return new WaitUntil(() => _player);
    }
    
    private EnemyController CreateEnemy()
    {
        var enemy = _stopSpawning ? Instantiate(bossPrefab) : Instantiate(enemyPrefab);
        enemy.player = _player;
        enemy.Pool = _enemyPool;
        return enemy;
    }
    
    private void OnGetEnemy(EnemyController enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnReturnEnemy(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public IEnumerator InstantiateBoss()
    {
        _stopSpawning = true;
        _enemyPool.Clear();
        bossPrefab = (BossController)_enemyPool.Get();
        bossPrefab.transform.position = new Vector3(15f, 0f, 0f);
        yield return new WaitUntil(() => bossPrefab.IsReady);
        _stopSpawning = false;
        _secondsBetweenSpawn /= 2f;
        StartCoroutine(InstantiateEnemies());
    }

    public bool IsBossDead() => bossPrefab.IsBossDead();
    
    private IEnumerator InstantiateEnemies()
    {
        var enemy = _enemyPool.Get();
        enemy.transform.position = new Vector3(11f, Random.Range(-8f, 8f), 0f);
        if (bossPrefab.IsReady)
            enemy.speed = 4;
        yield return new WaitForSeconds(_secondsBetweenSpawn);
        if (!_stopSpawning)
            StartCoroutine(InstantiateEnemies());
        yield return null;
    }
}
