using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    CapsuleCollider2D _capsuleCollider2D;
    PlayerInputManager _input;
    Rigidbody2D _rigidbody2D;

    [Header("StatusChecking")]
    [SerializeField] bool isGround = true;
    [SerializeField] bool isAttack = false;

    [Header("StatesChecking")]
    [SerializeField] bool inOnGroundState;

    [Header("GeneralSetting")]
    [SerializeField] GameObject _mainCamera;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject kunaiPrefab;
    [SerializeField] Transform kunaiSpawn;
    [SerializeField] GameObject attackPrefab;
    Vector3 savePoint;
    [HideInInspector] public int coin;
    RaycastHit2D hit;

    private float horizontal;
    private bool isJump = false;
    IStatePlayer currentState;

    // animation IDs
    int animIDIdle;
    int animIDRun;
    int animIDJump;
    int animIDInAir;
    int animIDFly;
    int animIDAttack;
    int animIDShoot;
    int animIDDeath;

    // Error start position Player
    public override void Start()
    {
        base.Start();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _input = GetComponent<PlayerInputManager>();
        _mainCamera = FindObjectOfType<CameraFollow>().gameObject;
        AssignAnimationIDs();
        SavePoint();
        coin = 0;
        UIManager.instance.SetCoin(coin);
    }
    public override void OnInit()
    {
        base.OnInit();
        isAttack = false;
        transform.position = savePoint;
        ChangeAnim(animIDIdle);
        ChangeState(new OnGroundStatePlayer());
        attackPrefab.SetActive(false);
    }

    public override void OnDespawn()
    {
        OnInit();
        base.OnDespawn();
    }

    protected override void OnDead()
    {
        base.OnDead();
        ChangeAnim(animIDDeath);
    }
    void FixedUpdate()
    {
        //UpdateOnGroundState();
        horizontal = Input.GetAxisRaw("Horizontal");
        isGround = GroundCheck();
        if (isGround)
        {
            if (isJump)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim(animIDRun);
            }
            if (isAttack)
            {
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                Shoot();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Attack();
            }
            if (isDead)
            {
                OnDead();
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }
            
        }
        if (!isGround && _rigidbody2D.velocity.y < 0)
        {
            ChangeAnim(animIDInAir);
            isJump = false;
        }
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            _rigidbody2D.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, _rigidbody2D.velocity.y);
            transform.rotation = Quaternion.Euler(0, horizontal > 0 ? 0 : 180, 0);
        }
        else if (isGround)
        {
            ChangeAnim(animIDIdle);
            _rigidbody2D.velocity = Vector2.zero;
        }
        
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }



    public void StartOnGroundState()
    {
        inOnGroundState = true;
    }
    public void UpdateOnGroundState()
    {
        isGround = GroundCheck();
    }
    public void ExitOnGroundState()
    {
        inOnGroundState = false;
    }

    public void ChangeState(IStatePlayer newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }


        void Attack()
        {
            isAttack = true;
            ChangeAnim(animIDAttack);
            attackPrefab.SetActive(true);
            Invoke(nameof(ResetAttack), 0.6f);
        }

        void ResetAttack()
        {
            isAttack = false;
            attackPrefab.SetActive(false);
            ChangeAnim(animIDIdle);
        }
    private void Jump()
    {
        isJump = true;
        ChangeAnim(animIDJump);
        _rigidbody2D.AddForce(jumpForce * Vector2.up);
    }
    void Shoot()
        {
            isAttack = true;
            ChangeAnim(animIDShoot);
            Invoke(nameof(ResetAttack), 0.6f);
            Instantiate(kunaiPrefab, kunaiSpawn.position, transform.rotation);
        }
        void SavePoint()
        {
            savePoint = transform.position;
        }
        bool GroundCheck()
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
            return hit.collider != null;
        }
        void AssignAnimationIDs()
        {
            animIDIdle = Animator.StringToHash("Idle");
            animIDRun = Animator.StringToHash("Run");
            animIDJump = Animator.StringToHash("StartJump");
            animIDFly = Animator.StringToHash("Fly");
            animIDInAir = Animator.StringToHash("InAir");
            animIDAttack = Animator.StringToHash("Attack");
            animIDShoot = Animator.StringToHash("Shoot");
            animIDDeath = Animator.StringToHash("Death");
            currentAnimID = animIDIdle;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Coin")
            {
                coin++;
                UIManager.instance.SetCoin(coin);
                Destroy(collision.gameObject);
            }
            if (collision.tag == "DeathZone")
            {
                hp = 0;
                ChangeAnim(animIDDeath);
                Invoke(nameof(OnInit), 1f);
            }
            if (collision.tag == "SavePoint")
            {
                savePoint = collision.transform.position;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Elevator")
            {
                transform.SetParent(collision.transform);
                _mainCamera.transform.SetParent(collision.transform);
            }
        }
        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Elevator")
            {
                transform.SetParent(null);
                _mainCamera.transform.SetParent(null);
            }
        }
    }

