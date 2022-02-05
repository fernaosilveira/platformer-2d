using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startLife = 3;
    public bool destroyOnKill = false;

    private int _currentLife;
    private bool _isDead = false;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _isDead = false;
        _currentLife = startLife;
    }

    public void Damage(int damage)
    {
        if (_isDead)
        {
            return;
        }
        _currentLife -= damage;
        if(_currentLife <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        _isDead = true;
        if (destroyOnKill)
        {
            Destroy(gameObject);
        }

    }
}
