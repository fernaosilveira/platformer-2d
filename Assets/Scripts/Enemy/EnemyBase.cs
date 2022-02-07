using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;
    private bool _playerAttack = false;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _playerAttack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(!_playerAttack)
        {
            var health = collision.gameObject.GetComponent<HealthBase>();
            if (health != null)
            {
                health.Damage(damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerAttack = true;
    }
}
