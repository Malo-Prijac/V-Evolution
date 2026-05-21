using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeStat : Upgrade
{
    [SerializeField]private Statistics _statisticsUpgrade;
    [SerializeField]private float valuePercent;
    enum Statistics
    {
        Range,
        Damage,
        MoveSpeed,
        AttackSpeed,
        HealthPoints
    };
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void UseUpgrade(GameObject playerGameObject)
    {
        PlayerController player;
        Weapon weapon;
        PlayerLife life;

        switch (_statisticsUpgrade)
        {
            case Statistics.Range :
                weapon = playerGameObject.GetComponent<Weapon>();
                weapon.Range += weapon.BaseRange * FromPercentToFactor(valuePercent);
                break;
            
            case Statistics.Damage :
                weapon = playerGameObject.GetComponent<Weapon>();
                weapon.Damage += weapon.BaseDamage * FromPercentToFactor(valuePercent);
                break;
            
            case Statistics.MoveSpeed :
                player = playerGameObject.GetComponent<PlayerController>();
                player.MoveSpeed += player.BaseMoveSpeed * FromPercentToFactor(valuePercent);
                break;
            
            case Statistics.AttackSpeed :
                weapon = playerGameObject.GetComponent<Weapon>();
                weapon.DelayShoot -=  weapon.BaseDelayShoot * FromPercentToFactor(valuePercent);
                break;
            
            case Statistics.HealthPoints :
                life = playerGameObject.GetComponent<PlayerLife>();
                life.IncreaseMaxHealthPercent(FromPercentToFactor(valuePercent));
                break;
        }
    }
    
    private float FromPercentToFactor(float percent)
    {
        return (percent / 100f);
    }
}
