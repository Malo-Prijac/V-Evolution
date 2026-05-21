using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyController : MonoBehaviour
{
    private PlayerController _playerController;


    
    [Header("Enemy Stats")]
    [SerializeField] private float hp = 3f;
    [SerializeField] private float attackDamage ;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float delayAttack = 0.5f;
    [SerializeField] private float rangeFactor = 1.5f;
    [SerializeField] private float speed = 3f;

    [Header("Enemy distance parameters")]
    [SerializeField] private bool isRange = false;
    [SerializeField] private GameObject projectilePrefab;
    
    [Header("Enemy Loot")]
    [SerializeField] private GameObject lootPrefab;


    private float attackTimer = 0.0f;
    private PlayerLife playerlife;
    private SpriteRenderer orientation;
    private Animator animator;
    private ControllerState _enemyState;
    private Rigidbody2D _rb;
    private Coroutine _attackCoroutine;
    private GameObject _lootEnemy;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        orientation = GetComponent<SpriteRenderer>();
        playerlife = FindObjectOfType<PlayerLife>();
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyState = ControllerState.IsMoving;
    }

    // Update is called once per frame
    void Update()
    {
        MachineState();
    }

    private void MachineState()
    {
        switch (_enemyState)
        {
            case ControllerState.IsMoving :
                
                if (IsPlayerInRangeToAttack())
                {
                    _attackCoroutine = StartCoroutine(Attack());
                    _enemyState = ControllerState.IsAttacking;
                }
                break;
            
            case ControllerState.IsAttacking :
                
                break;
            
            case ControllerState.IsDead :
                break;
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == false)
            return;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.OwnerTag = gameObject.tag;
        projectileScript.TargetTag = _playerController.tag;
        projectileScript.Damage = attackDamage;
        projectileScript.SetDirection(_playerController.transform.position, transform.position);
    }

    private bool IsPlayerInRangeToAttack()
    {
        float distance = Vector2.Distance(transform.position, _playerController.transform.position);
        return distance <= attackRange;
    }
    
    private bool IsPlayerInRangeToTakeDamage()
    {
        float distance = Vector2.Distance(transform.position, _playerController.transform.position);
        return distance <= attackRange * rangeFactor;
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    public IEnumerator Attack()
    {
        _enemyState = ControllerState.IsAttacking;
        animator.SetBool("isAttacking", true);
        
        yield return new WaitForSeconds(delayAttack);
        
        if (isRange)
        {
            Shoot();
        }
        else
        {
            if (IsPlayerInRangeToTakeDamage())
            {
                playerlife.TakeDamage(attackDamage);
            }
        }
    }
    
    private void MoveToPlayer()
    {
        if (!_playerController)
            return;

        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = _playerController.transform.position;
        
        orientation.flipX = (currentPosition.x > targetPosition.x);
        _rb.velocity = Vector2.zero;
        
        if (_enemyState != ControllerState.IsMoving)
            return;

        Vector2 direction = (targetPosition - currentPosition).normalized;
        _rb.velocity = Time.fixedDeltaTime * 100f * speed * direction;
    }
    
    public void TakeDamage(float damageTaken)
    {
        hp -= damageTaken;
        if (hp <= 0)
        {
            Death();
            _enemyState = ControllerState.IsDead;
        }
    }
 
    private void Death()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        
        WaveManager.Instance.CurrentWave.enemies.Remove(gameObject);
        animator.SetBool("isDead",true);
        speed = 0;
        _rb.simulated = false;
    }

    public void LootObject()
    {
        _lootEnemy = Instantiate(lootPrefab, transform.position, Quaternion.identity);
    }
    
    public void EndAnimation(string message)
    {
        if (message.Equals("DeathAnimationEnded"))
        {
            gameObject.SetActive(false);
            LootObject();
            gameObject.SetActive(false);
        } 
        else if (message.Equals("AttackAnimationEnded"))
        {
            animator.SetBool("isAttacking", false);
            _enemyState = ControllerState.IsMoving;
        }
    }
}
