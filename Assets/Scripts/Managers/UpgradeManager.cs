using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance;

    [SerializeField]private List<Upgrade> upgrades = new List<Upgrade>();
    [SerializeField] private TextMeshProUGUI coinCounterText;
    private GameObject _playerGameObject;

    private int _currentMoney;

    private List<UpgradeButton> _upgradeButtons = new List<UpgradeButton>();

    public static UpgradeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UpgradeManager>();

                if (_instance == null)
                {
                    _instance = new GameObject("UpgradeManager").AddComponent<UpgradeManager>();
                }
            }

            return _instance;
        }
    }
    
    public int CurrentMoney
    {
        get => _currentMoney;
        set => _currentMoney = value;
    }

    public List<UpgradeButton> UpgradeButtons
    {
        get => _upgradeButtons;
        set => _upgradeButtons = value;
    }

    private void Awake()
    {
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
    
    // Start is called before the first frame update
    void Start()
    {
        _playerGameObject = FindObjectOfType<PlayerController>().gameObject;
    }

    public void AddMoney(int amount)
    {
        _currentMoney += amount;
        SetCurrentMoneyText();
    }
    
    public void UseMoney(int amount)
    {
        AddMoney(-amount);
    }

    private void SetCurrentMoneyText()
    {
        string coinString = _currentMoney.ToString("00000");
        coinCounterText.SetText(coinString);
    }

    public void ResetMoney()
    {
        _currentMoney = 0;
        SetCurrentMoneyText();
    }

    public Upgrade DrawUpgrade()
    {
        int random = Random.Range (0, upgrades.Count);
        
        if(upgrades[random] == false)
            print("NO UPGRADES");
        
        return upgrades[random];
    }

    public void RedrawAllUpgrade()
    {
        foreach (UpgradeButton upgradeButton in UpgradeButtons)
        {
            upgradeButton.DrawUpgrade();
        }
    }


    public bool CanPurchaseUpgrade(int cost)
    {
        return (cost <= _currentMoney);
    }

    public void ResetAllUpgrades()
    {
        PlayerController player = _playerGameObject.GetComponent<PlayerController>();
        Weapon weapon = _playerGameObject.GetComponent<Weapon>();
        PlayerLife life = _playerGameObject.GetComponent<PlayerLife>();;

        player.ResetStat();
        weapon.ResetStat();
        life.ResetLife();
    }
}
