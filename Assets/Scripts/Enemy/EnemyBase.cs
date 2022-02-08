using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;

    [Header("Detection circle")]
    public float centerOffset;
    public float radius;
    public LayerMask whatIsPlayer;

    private bool IsPlayerAttack()
    {
        return Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - centerOffset), radius, whatIsPlayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(!IsPlayerAttack())
        {
            var health = collision.gameObject.GetComponent<HealthBase>();
            if (health != null)
            {
                health.Damage(damage);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsPlayerAttack() ? Gizmos.color = Color.red : Color.yellow;

        Vector2 currentPosition = transform.position;
        currentPosition.y = transform.position.y - centerOffset;

        Gizmos.DrawWireSphere(currentPosition, radius);
    }

}
