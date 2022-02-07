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

    private float _currentSpeed;
    private bool _isGrounded;
    private Vector2 _baseScale;

    private void Start()
    {
        _baseScale = myRigidbody.transform.localScale;
    }
    void Update()
    {
        HandleJump();
        HandleMovement();
        
    }

    private void HandleMovement()
    {
        if (_isGrounded)
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
        if (_isGrounded)
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
       
        if (myRigidbody.velocity.y < 0)
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            _isGrounded = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            _isGrounded = true;
        }
   
        FallInpactAnimation();

        if (collision.tag == enemyTag)
        {
            OnEnemyKill();

            var health = collision.gameObject.GetComponent<HealthBase>();
            if (health != null)
            {
                health.Damage(playerDamage);
            }
        }
    }

}