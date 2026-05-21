using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    [SerializeField] private int _moneyAmount = 0;

    public int MoneyAmount
    {
        get => _moneyAmount;
        set => _moneyAmount = value;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
            return;
        
        UpgradeManager.Instance.AddMoney(MoneyAmount);
        Destroy(gameObject);
    }
}
