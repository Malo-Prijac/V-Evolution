using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float baseMaxLife = 10f;
    private float _maxLife;
    private float _currentLife;
    private Animator animator;
    
    [SerializeField] private Image healthBar;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        _maxLife = baseMaxLife;
        _currentLife = _maxLife;
        animator = GetComponent<Animator>();
        isDead = false;
    }

    public void TakeDamage(float damage) 
    {
        _currentLife -= damage;
        healthBar.fillAmount = _currentLife/_maxLife;

        if (_currentLife <= 0)
        {
            Death();
            _currentLife = 0;
        }
    }

    public void Death()
    {
        isDead = true;
        animator.SetBool("isDead",true);
        GetComponent<PlayerController>().Death();
    }

    public void EndAnimation(string message)
    {
        if (message.Equals("PlayerDeathAnimationEnded"))
        {
            GameManager.Instance.Defeat();
        }
    }

    public void ResetLife()
    {
        _maxLife = baseMaxLife;
        RegainLife();
    }

    public void RegainLife()
    {
        _currentLife = _maxLife;
        isDead = false;
        healthBar.fillAmount = _currentLife/_maxLife;
    }

    public void IncreaseMaxHealthPercent(float percent)
    {
        _maxLife += baseMaxLife * percent;
        _currentLife += baseMaxLife * percent;
        healthBar.fillAmount = _currentLife/_maxLife;
    }
    
    public void HealPlayer(float value)
    {
        _currentLife += value;
        healthBar.fillAmount = _currentLife/_maxLife;
    }
    
}
