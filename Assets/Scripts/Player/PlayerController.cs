using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private float baseMoveSpeed = 3.5f;
    private float _moveSpeed = 0;

    private Rigidbody2D _rb;
    private Vector2 _direction;

    private ControllerState _playerState;
    
    private SpriteRenderer orientation;
    private Animator animator;
    private Weapon _weapon;

    public float BaseMoveSpeed
    {
        get => baseMoveSpeed;
        set => baseMoveSpeed = value;
    }
    
    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    public DynamicJoystick Joystick
    {
        get => joystick;
        set => joystick = value;
    }

    public ControllerState PlayerState
    {
        get => _playerState;
        set => _playerState = value;
    }

    // Start is called before the first frame update

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        orientation = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _weapon = GetComponent<Weapon>();
    }

    void Start()
    {
        PlayerState = ControllerState.IsMoving;
        _moveSpeed = BaseMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        InputPlayer();
        MachineState();
    }

    private void MachineState()
    {
        switch (PlayerState)
        {
            case ControllerState.IsAttacking :
                break;

            case ControllerState.IsMoving :
                break;

            case ControllerState.IsDead :
                break;
        }
    }


    void InputPlayer()
    {

        _direction = (Vector2.up * Joystick.Vertical  + Vector2.right * Joystick.Horizontal).normalized;
        
        orientation.flipX = (Joystick.Horizontal < 0);
        animator.SetBool("isMoving", _direction != Vector2.zero);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (PlayerState != ControllerState.IsMoving)
            return;
        
        _rb.velocity = Time.fixedDeltaTime * 100f * MoveSpeed * _direction;
    }

    public void ResetAndRespawn()
    {
        UpgradeManager.Instance.ResetAllUpgrades();
        Respawn();
    }
    
    public void Respawn()
    {
        GetComponent<PlayerLife>().RegainLife();
        animator.SetBool("isDead",false);
        transform.position = Vector3.zero;
        PlayerState = ControllerState.IsMoving;
        GetComponent<Rigidbody2D>().simulated = true;
        _weapon.enabled = true;
    }

    public void Death()
    {
        _weapon.enabled = false;
        PlayerState = ControllerState.IsDead;
        GetComponent<Rigidbody2D>().simulated = false;
    }

    public void ResetStat()
    {
        _moveSpeed = BaseMoveSpeed;
    }
}
