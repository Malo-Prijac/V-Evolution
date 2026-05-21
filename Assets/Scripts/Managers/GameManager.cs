using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private GameObject uiVictoryScreen;
    [SerializeField] private GameObject uiDefeatScreen;
    [SerializeField] private GameObject uiStartGame;
    private PlayerLife _playerLife;
    private PlayerController _playerController;
    private DynamicJoystick joystick;
    private WaveManager waveManager;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    _instance = singleton.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }
    
    private void Awake()
    {
        waveManager = WaveManager.Instance;
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _playerController = FindObjectOfType<PlayerController>();
        _playerLife = _playerController.GetComponent<PlayerLife>();
    }
    
    void Start()
    {
        uiDefeatScreen.SetActive(false);
        uiVictoryScreen.SetActive(false);
        uiStartGame.SetActive(true);
        EnableJoystick(false);

        Time.timeScale = 0f;
    }

    public void StartFirstWave()
    {
        uiStartGame.SetActive(false);
        EnableJoystick(true);
        waveManager.StartWave();
        Time.timeScale = 1f;
    }

    public void EnableJoystick(bool enable)
    {
        _playerController.Joystick.gameObject.SetActive(enable);
    }
    
    public void Victory()
    {
        EnableJoystick(false);
        Time.timeScale = 0;
        uiVictoryScreen.SetActive(true);
    }
    
    public void Defeat()
    {
        EnableJoystick(false);
        Time.timeScale = 0;
        uiDefeatScreen.SetActive(true);
    }

    public void RestartWave()
    {
        RemoveAllLoots();
        RemoveAllProjectiles();
            
        uiDefeatScreen.SetActive(false);
        uiVictoryScreen.SetActive(false);
        waveManager.RestartCurrentWave();

        FindObjectOfType<PlayerController>().Respawn();
        EnableJoystick(true);
        Time.timeScale = 1;
    }

    public void ResetAndRestart()
    {
        UpgradeManager.Instance.ResetMoney();
        
        RemoveAllLoots();
        RemoveAllProjectiles();
        RemoveAllEnemies();
        
        uiDefeatScreen.SetActive(false);
        uiVictoryScreen.SetActive(false);
        waveManager.ResetWavesAndRestart();

        FindObjectOfType<PlayerController>().ResetAndRespawn();
        EnableJoystick(true);
        Time.timeScale = 1;
    }

    public bool IsPlayerDead()
    {
        return _playerLife.isDead;
    }

    public void RemoveAllProjectiles()
    {
        Projectile[] projectiles = FindObjectsOfType<Projectile>();

        foreach (Projectile projectile in projectiles)
        {
            Destroy(projectile.gameObject);
        }
    }
    
    public void RemoveAllLoots()
    {
        LootObject[] loots = FindObjectsOfType<LootObject>();

        foreach (LootObject loot in loots)
        {
            Destroy(loot.gameObject);
        }
    }
    
    public void RemoveAllEnemies()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        foreach (EnemyController enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
