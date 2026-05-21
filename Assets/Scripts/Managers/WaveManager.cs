using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    private float time;
    private bool _timeRunning = true;
    private WaveSpawner _waveSpawner;
    private WaveSpawner.Wave _currentWave;
    
    [Header("Next Wave UI")]
    [SerializeField] private GameObject uiBetweenWaves;

    [Header("Wave timer UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    
    [Header("Wave counter UI")]
    [SerializeField] private TextMeshProUGUI waveCounterText;

    [Header("Percentages texts")]
    [SerializeField] private TextMeshProUGUI tankText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private TextMeshProUGUI dpsText;

    [Header("Time limit when the centiseconds appear")]
    [SerializeField] private int secondsLimit = 5;
    
    public WaveSpawner.Wave CurrentWave
    {
        get => _currentWave;
        set => _currentWave = value;
    }

    private static WaveManager _instance;

    public static WaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WaveManager>();

                if (_instance == null)
                {
                    _instance = new GameObject("WaveManager").AddComponent<WaveManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _waveSpawner = FindObjectOfType<WaveSpawner>();
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        _waveSpawner.currentWaveNumber = 0; 
        uiBetweenWaves.SetActive(false);
        EnableTimer(false, 0);
    }
    
    void Update()
    {
        Timer();
    }

    private void EnableTimer(bool enable, int waveNumber)
    {
        Time.timeScale = enable ? 1.0f : 0f;
        time = enable ? _waveSpawner.waves[waveNumber].duration : 0f;
        timerText.gameObject.SetActive(enable);
        SetTimerText();
        _timeRunning = enable;
    }

    public void Timer()
    {
        if (!_timeRunning)
            return;

        if (GameManager.Instance.IsPlayerDead())
            return;
        
        SetTimerText();
        
        time = time > 0 ? time -= Time.deltaTime : time;
        time = time < 0 ? 0 : time;

        if (time > 0)
            return;
        
        if(_waveSpawner.currentWaveNumber >= _waveSpawner.waves.Length - 1)
        {
            GameManager.Instance.Victory();
            EnableTimer(false, _waveSpawner.currentWaveNumber);
        }
        else
        {
            ChangePercentage();
            DisplayUIBetweenWaves(true, _waveSpawner.currentWaveNumber + 1);
        }
    }
    
    private void SetTimerText()
    {
        int seconds = Mathf.RoundToInt(time);
        string stringTimer = seconds.ToString("00");
        timerText.color= new Color32(0,0,0,255);
        if (seconds < secondsLimit)
        {
            float centiSeconds = Mathf.RoundToInt((time * 100.0f) % 100);
            stringTimer += ":" + centiSeconds.ToString("00");
            timerText.color= new Color32(255,0,0,255);
        }

        timerText.text = stringTimer;
    }
    
    private void DisplayUIBetweenWaves(bool display, int waveNumber)
    {
        if (display) { UpgradeManager.Instance.RedrawAllUpgrade();}
        
        GameManager.Instance.EnableJoystick(!display);
        uiBetweenWaves.SetActive(display);
        _timeRunning = !display;
        EnableTimer(!display, waveNumber);
    }

    public void NextWave()
    {
        _waveSpawner.ResetWaveNumber(_waveSpawner.currentWaveNumber);
        _waveSpawner.currentWaveNumber++;

        if (_waveSpawner.currentWaveNumber < _waveSpawner.waves.Length)
        {
            StartWave();
            DisplayUIBetweenWaves(false, _waveSpawner.currentWaveNumber);
        }
    }

    public void RestartCurrentWave()
    {
        _waveSpawner.ResetWaveNumber(_waveSpawner.currentWaveNumber);
        _currentWave.InitWave();
        StartWave();
    }

    private void SetCurrentWaveText(int currentWave)
    {
        waveCounterText.SetText("Wave "+ (currentWave+1));
    }

    private void ChangePercentage()
    {
        float tankRate = 0f;
        float rangeRate = 0f;
        float dpsRate = 0f;
        
        foreach (var enemySpawning in _waveSpawner.GetWaveNumber(_waveSpawner.currentWaveNumber + 1).enemiesSpawning)
        {
            switch (enemySpawning.Type)
            {
                case WaveSpawner.EnemyType.TANK:
                    tankRate += enemySpawning.rate*100f;
                    break;
                
                case WaveSpawner.EnemyType.RANGED:
                    rangeRate += enemySpawning.rate*100f;
                    break;
                
                case WaveSpawner.EnemyType.DPS:
                    dpsRate += enemySpawning.rate*100f;
                    break;
            }
        }
        
        tankText.SetText("Tank: "+ tankRate + "%");
        rangeText.SetText("Range: "+ rangeRate + "%");
        dpsText.SetText("Dps: "+ dpsRate + "%");
    }

    public void StartWave()
    {
        CurrentWave = _waveSpawner.SpawnWaveNumber(_waveSpawner.currentWaveNumber);
        EnableTimer(true, _waveSpawner.currentWaveNumber);
        SetCurrentWaveText(_waveSpawner.currentWaveNumber);
    }
    
    public void ResetWaves()
    {
        _waveSpawner.ResetAllWaves();
        _waveSpawner.InitAllWaves();
        _waveSpawner.currentWaveNumber = 0;
    }

    public void ResetWavesAndRestart()
    {
        ResetWaves();
        StartWave();
    }
}
