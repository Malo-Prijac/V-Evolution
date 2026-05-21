using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 normalizeDirection;
    private float _damage;

    private string _ownerTag;

    private string _targetTag;
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }

    public string TargetTag
    {
        get => _targetTag;
        set => _targetTag = value;
    }

    public string OwnerTag
    {
        get => _ownerTag;
        set => _ownerTag = value;
    }

    //private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += normalizeDirection * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 targetPosition, Vector3 currentPosition)
    {
        normalizeDirection = (targetPosition - currentPosition).normalized;
        transform.LookAt(targetPosition);

        float rotationAngle = Mathf.Atan2(normalizeDirection.y, normalizeDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TargetTag))
        {
            if(col.gameObject.GetComponent<EnemyController>())
                col.gameObject.GetComponent<EnemyController>().TakeDamage(_damage);
            
            if(col.gameObject.GetComponent<PlayerLife>())
                col.gameObject.GetComponent<PlayerLife>().TakeDamage(_damage);
        }
        
        if (!col.CompareTag(_ownerTag))
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
