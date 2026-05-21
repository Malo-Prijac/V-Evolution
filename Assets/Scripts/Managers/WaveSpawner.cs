using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{

    public enum EnemyType
    {
        TANK,
        RANGED,
        DPS,
    };

    [Serializable]
    public class EnemySpawning
    {
        [SerializeField] private string name;
        [SerializeField] private EnemyType type;

        
        [SerializeField] public GameObject enemyPrefab;
        [SerializeField][Range(0,1)] public float rate;
        private int _nbEnemyToSpawn;

        public int NbEnemyToSpawn
        {
            get => _nbEnemyToSpawn;
            set => _nbEnemyToSpawn = value;
        }

        public EnemyType Type
        {
            get => type;
            set => type = value;
        }
    };
    
    [Serializable]
    public class Wave
    {
        [SerializeField] private string name;

        [Header("Duration of the wave in seconds")] 
        public float duration = 30f;
        public int enemyLeftToSpawn = 0;

        [Header("Spawning")] 
        [SerializeField]public List<EnemySpawning> enemiesSpawning;

        [Header("Number of enemies")]
        [SerializeField]public int enemyNumber = 10;

        public float spawningRate;

        public List<GameObject> enemies;

        public void InitWave()
        {
            enemies = new List<GameObject>();
            foreach (var enemySpawning in enemiesSpawning)
            {
                enemySpawning.NbEnemyToSpawn = Mathf.RoundToInt(enemySpawning.rate * enemyNumber);
                enemyLeftToSpawn += enemySpawning.NbEnemyToSpawn;
            }

            spawningRate = enemyNumber/ duration;

            while( enemyLeftToSpawn > 0)
            {
                int random = Random.Range(0, 3);
                EnemySpawning enemySpawning = enemiesSpawning[random];
            
                if (enemySpawning.NbEnemyToSpawn <= 0)
                    continue;
            
                CreateEnemy(enemySpawning.enemyPrefab);
                enemyLeftToSpawn--;
                enemySpawning.NbEnemyToSpawn--;
            }
        }

        private void CreateEnemy(GameObject enemyPrefab)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemyPrefab.transform.position, enemyPrefab.transform.rotation);
            enemy.SetActive(false);
            enemies.Add(enemy);
        }
    }

    [SerializeField] public int currentWaveNumber = 0;
    
    [Header("Spawn parameters")]
    [SerializeField] private float minRadiusSpawn = 20;
    [SerializeField] private float maxRadiusSpawn = 40;
    [SerializeField] public Wave[] waves;

    private Transform _player;

    private Coroutine _spawnWave;

    // Start is called before the first frame update
    void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    private void Start()
    {
        InitAllWaves();
    }

    public void InitAllWaves()
    {
        foreach (var wave in waves)
        {
            wave.InitWave();
        }
    }

    public Wave SpawnWaveNumber(int nbWave)
    {
        if (_spawnWave != null)
        {
            StopCoroutine(_spawnWave);
        }
        
        GameManager.Instance.RemoveAllProjectiles();
        _spawnWave = StartCoroutine(SpawnWave(waves[nbWave]));
        return waves[nbWave];
    }

    public Wave GetWaveNumber(int nbWave)
    {
        return waves[nbWave];
    }

    public IEnumerator SpawnWave(Wave wave)
    {
        for (int i = wave.enemies.Count - 1; i >= 0 ; i--)
        {
            SpawnEnemy(wave.enemies[i]);
            yield return new WaitForSeconds(1f/wave.spawningRate);
        }
        
        yield break;
    }
    
    void SpawnEnemy(GameObject enemy)
    {
        if (enemy == false)
            return;

        Vector2 randomPos = new Vector2(Random.value - 0.5f, Random.value - 0.5f).normalized *
                            Random.Range(minRadiusSpawn, maxRadiusSpawn);
        
        Vector2 posPlayer = _player.position;
        enemy.transform.position = randomPos + posPlayer;
        enemy.SetActive(true);
    }

    public void ResetWave(Wave wave)
    {
        foreach (GameObject enemy in wave.enemies)
        {
            Destroy(enemy);
        }
    }
    
    public void ResetAllWaves()
    {
        foreach (Wave wave in waves)
        {
            ResetWave(wave);
        }
    }
    
    public void ResetWaveNumber(int nbWave)
    {
        ResetWave(waves[nbWave]);
    }
}
