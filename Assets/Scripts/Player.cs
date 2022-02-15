using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D myRigidbody;
    

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

    [Header("Detection area")]
    public float centerOffset;
    public float radius;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;

    [Header("Animator Player")]
    public Animator animator;
    public string boolRun = "Run";
    public float runSpeed = 1.5f;
    public float turnDuration = .2f;

    private float _currentSpeed;
    private Vector2 _baseScale;
    private Vector2 _turnScale;


    private void Start()
    {
        _baseScale = myRigidbody.transform.localScale;
        _turnScale = new Vector2(-_baseScale.x, _baseScale.y);

    }
    void Update()
    {
        HandleMovement();
        HandleJump();
        FallInpactAnimation();
    }

    private void HandleMovement()
    {

        if (Input.GetKey(KeyCode.A))
        {
            myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
            if (myRigidbody.transform.localScale.x != -_baseScale.x)
            {
                myRigidbody.transform.DOScaleX(-_baseScale.x, turnDuration);
            }
            animator.SetBool(boolRun, true);
            if (!IsGrounded())
            {
                animator.SetBool(boolRun, false);
            }
            

        }

        else if (Input.GetKey(KeyCode.D))
        {
            myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
            myRigidbody.transform.DOScaleX(_baseScale.x, turnDuration);
            if (myRigidbody.transform.localScale.x != _baseScale.x)
            {
                myRigidbody.transform.DOScaleX(_baseScale.x, turnDuration);
            }
            animator.SetBool(boolRun, true);
            if (!IsGrounded())
            {
                animator.SetBool(boolRun, false);
            }
        }

        else
        {
            animator.SetBool(boolRun, false);
        }

        Friction();
        HandleRun();
    }

    private void Friction()
    {
        if (myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity -= friction;
        }

        else if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity += friction;
        }
    }
    private void HandleRun()
    {
        if (IsGrounded() && Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speedRun;
            animator.speed = runSpeed;
        }

        else
        {
            _currentSpeed = speed;
            animator.speed = 1;
        }
    }

    private void HandleJump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * jumpForce;
            if (myRigidbody.transform.localScale.x < 0)
            {
                myRigidbody.transform.localScale = _turnScale;
            }
            else
            {
                myRigidbody.transform.localScale = _baseScale;
            }
            DOTween.Kill(myRigidbody.transform);
            JumpAnimation();
        }    
    }

    private void JumpAnimation()
    {
        if (myRigidbody.transform.localScale.x < 0)
        {
            myRigidbody.transform.DOScaleY(jumpScaleY, jumpduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            myRigidbody.transform.DOScaleX(-jumpScaleX, jumpduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        }
        else
        {
            myRigidbody.transform.DOScaleY(jumpScaleY, jumpduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            myRigidbody.transform.DOScaleX(jumpScaleX, jumpduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        }
           
    }

    private void FallInpactAnimation()
    {
        if (IsGrounded() && myRigidbody.velocity.y <= -2f)
        {
            if(myRigidbody.transform.localScale.x < 0)
            {
                myRigidbody.transform.localScale = _turnScale;
                DOTween.Kill(myRigidbody.transform);
                myRigidbody.transform.DOScaleY(fallScaleY, fallduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
                myRigidbody.transform.DOScaleX(-fallScaleX, fallduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            }
            else
            {
                myRigidbody.transform.localScale = _baseScale;
                DOTween.Kill(myRigidbody.transform);
                myRigidbody.transform.DOScaleY(fallScaleY, fallduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
                myRigidbody.transform.DOScaleX(fallScaleX, fallduration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            }
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
        return Physics2D.OverlapCircle(new Vector2 (transform.position.x, transform.position.y - centerOffset), radius, whatIsGround);
    }

    private bool JumpOnEnemy()
    {
        return Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - centerOffset), radius, whatIsEnemy);
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