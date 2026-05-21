using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float baseRange = 10f;
    [SerializeField] private float baseDelayShoot = 0.8f;
    [SerializeField] private float baseDamage = 1f;
    
    private float _range = 10f;
    private float _delayShoot = 1f;
    private float _damage = 1f;
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField]private string enemyTag = "Enemy";
    private List<GameObject> enemies;
    private WaveManager _waveManager;
    private bool _canShoot;

    private GameObject closestEnemy;

    private List<GameObject> enemiesInRange;
    
    public float BaseRange
    {
        get => baseRange;
        set => baseRange = value;
    }

    public float BaseDelayShoot
    {
        get => baseDelayShoot;
        set => baseDelayShoot = value;
    }
    
    public float BaseDamage
    {
        get => baseDamage;
        set => baseDamage = value;
    }
    
    public float Range
    {
        get => _range;
        set => _range = value;
    }

    public float DelayShoot
    {
        get => _delayShoot;
        set => _delayShoot = value;
    }

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        enemiesInRange = new List<GameObject>();
        
        _waveManager = WaveManager.Instance;
        _canShoot = true;
        Range = BaseRange;
        DelayShoot = BaseDelayShoot;
        Damage = BaseDamage;
    }

    // Update is called once per frame
    void Update()
    {
        AttackClosestEnemy();
    }

    private void AttackClosestEnemy()
    {
        if (!_canShoot)
            return;

        if (_waveManager.CurrentWave == null)
            return;
        
        enemies = _waveManager.CurrentWave.enemies;

        AddEnemiesInRange();
        closestEnemy = GetClosestEnemy();
        if (closestEnemy != null)
        {
            Shoot(closestEnemy.transform.position);
            _canShoot = false;
        }
    }


    private GameObject GetClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        closestEnemy = null;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
    
    private void AddEnemiesInRange()
    {
        enemiesInRange.Clear();
        foreach (GameObject enemy in enemies)
        {
            if (!enemy)
                continue;
            
            if (!enemy.activeSelf)
                continue;

            if (IsInRange(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void Shoot(Vector3 enemy)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.OwnerTag = gameObject.tag;
        projectileScript.TargetTag = enemyTag;
        projectileScript.Damage = Damage;
        projectileScript.SetDirection(enemy, transform.position);

        StartCoroutine(ShootDelay());
    }
    
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(DelayShoot);
        _canShoot = true;
    }

    private bool IsInRange(GameObject enemy)
    {
        return (enemy.transform.position - transform.position).magnitude < Range;
    }

    public void ResetStat()
    {
        Damage = BaseDamage;
        Range = BaseRange;
        DelayShoot = BaseDelayShoot;
    }
}
