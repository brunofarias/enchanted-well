using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("Config Movimento")]
  [SerializeField] float xAxis;
  [SerializeField] float yAxis;
  [SerializeField] float walkSpeed;
  [SerializeField] float jumpForce;
  public Transform groundCheckLeft;
  public Transform groundCheckRight;
  public LayerMask groundLayer;

  [Header("Config Estado")]
  public bool isOld;
  public bool isAdult;
  public bool isChild;

  [Header("Config Animação")]
  public string currentState;
  const string PLAYER_IDLE = "idle";
  const string PLAYER_RUN = "run";
  const string PLAYER_JUMP = "jump";
  const string PLAYER_FALL = "fall";
  const string PLAYER_DIE = "Die";
  const string PLAYER_PUSH = "Push";
  public AnimatorOverrideController oldAnin;
  public AnimatorOverrideController adultAnin;
  public AnimatorOverrideController childAnin;

  private Rigidbody2D rb;
  private Animator animator;
  public bool isGrounded = true;
  private bool isJumpPressed = false;
  private bool isLeft = false;
  private int currentHealth;
  private int damage;
  public bool die;

  public GameObject oldWorld;
  public GameObject adultWorld;
  public GameObject childWorld;
  public bool isFreeze;

  private static PlayerController instance;

  public static PlayerController Instance
  {
    get
    {
      if (instance == null) instance = GameObject.FindObjectOfType<PlayerController>();
      return instance;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    isFreeze = false;

    isChild = true;
    isAdult = false;
    isOld = false;
    childWorld.SetActive(true);
    adultWorld.SetActive(false);
    oldWorld.SetActive(false);
    
  }

  // Update is called once per frame
  void Update()
  {
    GetInput();
  }

  void FixedUpdate()
  {
    if (!isFreeze)
    {
      DetectCollision();
      ApplyIdle();
      ApplyMovement();
      ApplyJumping();
      ApplyFall();
    }
  }

  void GetInput()
  {
    if (!isFreeze)
    {
      xAxis = Input.GetAxisRaw("Horizontal");
      yAxis = Input.GetAxisRaw("Vertical");

      //criança
      if (Input.GetKey(KeyCode.R))
      {
        isChild = true;
        isAdult = false;
        isOld = false;
        jumpForce = 400;
        walkSpeed = 4.5f;
        childWorld.SetActive(true);
        adultWorld.SetActive(false);
        oldWorld.SetActive(false);
        animator.runtimeAnimatorController = childAnin as RuntimeAnimatorController;
      }

      //adulto
      if (Input.GetKey(KeyCode.F))
      {
        isAdult = true;
        isOld = false;
        isChild = false;
        jumpForce = 300;
        walkSpeed = 4f;
        adultWorld.SetActive(true);
        childWorld.SetActive(false);
        oldWorld.SetActive(false);
        animator.runtimeAnimatorController = adultAnin as RuntimeAnimatorController;
      }

      //velho
      if (Input.GetKey(KeyCode.V))
      {
        if (isOld == true) 
        isOld = true;
        isAdult = false;
        isChild = false;
        jumpForce = 250;
        walkSpeed = 3f;
        oldWorld.SetActive(true);
        childWorld.SetActive(false);
        adultWorld.SetActive(false);
        animator.runtimeAnimatorController = oldAnin as RuntimeAnimatorController;
      }

      if (Input.GetButtonDown("Jump") && isGrounded)
      {
        isJumpPressed = true;
      }
    }
    else
    {
      xAxis = 0;
      if (isGrounded) rb.velocity = Vector2.zero;
    }
  }
  void DetectCollision()
  {
    isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position, groundLayer);
  }

  void ApplyIdle()
  {
    if (xAxis == 0 && yAxis == 0 && isGrounded)
    {
      ChangeAnimationState(PLAYER_IDLE);
    }
  }

  void ApplyMovement()
  {
    rb.velocity = new Vector2(xAxis * walkSpeed, rb.velocity.y);

    if (xAxis > 0 && isLeft)
    {
      Flip();
    }
    else if (xAxis < 0 && !isLeft)
    {
      Flip();
    }

    if (xAxis != 0 && yAxis >= 0 && isGrounded)
    {
      ChangeAnimationState(PLAYER_RUN);
    }
  }

  void ApplyJumping()
  {
    if (isJumpPressed)
    {
      isJumpPressed = false;
      rb.AddForce(new Vector2(0, jumpForce));
      ChangeAnimationState(PLAYER_JUMP);
    }
  }

  void ApplyFall()
  {
    if (rb.velocity.y < 0 && !isGrounded)
    {
      ChangeAnimationState(PLAYER_FALL);
    }
  }

  public void ApplyDie()
  {
    isFreeze = true;
    rb.velocity = Vector2.zero;
    ChangeAnimationState(PLAYER_DIE);
  }

  void Flip()
  {
    isLeft = !isLeft;
    float x = transform.localScale.x * -1;
    transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
  }

  void ChangeAnimationState(string newState)
  {
    if (currentState == newState) return;
    animator.Play(newState);
    currentState = newState;
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.DrawLine(groundCheckLeft.position, groundCheckRight.position);
  }

  public void closeGame()
  {
    Application.Quit();
  }
  
}