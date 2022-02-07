using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D myRigidbody;
    public string groundTag = "Ground";
    public string enemyTag = "Enemy";

    [Header("Movement Params")]
    public float speed;
    public float speedRun;
    public float jumpForce = 1f;
    public float enemyBounce = 10f;
    public Vector2 friction = new Vector2(.1f, 0);

    [Header("Animation Params")]
    public float jumpduration = .3f;
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float fallduration = .3f;
    public float fallScaleY = 0.6f;
    public float fallScaleX = 1.3f;
    public Ease ease = Ease.OutBack;

    [Header("Damage Params")]
    public int playerDamage = 10;

    [Header("Detection circle")]
    public float centerOffset;
    public float radius;

    private int _groundLayer = 6;
    private int _enemyLayer = 7;
    private float _currentSpeed;
    private Vector2 _baseScale;

    private void Start()
    {
        _baseScale = myRigidbody.transform.localScale;
    }
    void Update()
    {
        HandleMovement();
        HandleJump();
        FallInpactAnimation();
    }

    private void HandleMovement()
    {
        if (IsGrounded())
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentSpeed = speedRun;
            }
            else
            {
                _currentSpeed = speed;
            }

        }

        if (Input.GetKey(KeyCode.A))
        {
            myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
        }

        if(myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity -= friction;
        }

        else if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if (IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.velocity = Vector2.up * jumpForce;
                myRigidbody.transform.localScale = _baseScale;
                DOTween.Kill(myRigidbody.transform);
                JumpAnimation();
            }
        }    
    }

    private void JumpAnimation()
    {
        myRigidbody.transform.DOScaleY(jumpScaleY, jumpduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody.transform.DOScaleX(jumpScaleX, jumpduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    private void FallInpactAnimation()
    {
       
        if (myRigidbody.velocity.y < 0 && IsGrounded())
        {
            myRigidbody.transform.localScale = _baseScale;
            DOTween.Kill(myRigidbody.transform);
            myRigidbody.transform.DOScaleY(fallScaleY, fallduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            myRigidbody.transform.DOScaleX(fallScaleX, fallduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        }
    }

    private void OnEnemyKill()
    {
       
            myRigidbody.velocity = Vector2.up * enemyBounce;
            myRigidbody.transform.localScale = _baseScale;
            DOTween.Kill(myRigidbody.transform);
            JumpAnimation();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(new Vector2 (transform.position.x, transform.position.y - centerOffset), radius, _groundLayer);
    }

    private bool JumpOnEnemy()
    {
        return Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - centerOffset), radius, _enemyLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (JumpOnEnemy())
        {
            OnEnemyKill();
            var health = collision.gameObject.GetComponent<HealthBase>();
            if (health != null)
            {
                health.Damage(playerDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Gizmos.color = Color.red : Color.yellow;

        Vector2 currentPosition = transform.position;
        currentPosition.y = transform.position.y - centerOffset;

        Gizmos.DrawWireSphere(currentPosition, radius);
    }

}