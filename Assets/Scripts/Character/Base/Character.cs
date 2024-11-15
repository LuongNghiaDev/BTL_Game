using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Character : MonoBehaviour
{
    private bool isGround;
    private bool isFalling;
    private bool isRunning;
    private bool isClimb;
    private bool isDeath;
    private bool isJumping;
    private bool isFacingLeft = true;

    // no movement
    private bool isFreezeInputMovement;
    private bool isInSkillInterval;

    private bool isInputable;
    private bool isGameIntro;

    private float gravityScale;

    //Intro
    [SerializeField] float gameIntroInterval;

    [Space]

    [SerializeField] CameraShake cameraShake;
    [SerializeField] float deathEffectInterval;
    private bool isInvincible;
    private Vector3 recoverPosition;

    private Transform controllerTransform;
    private Rigidbody2D controllerRigidbody;
    private SpriteRenderer controllerSpriteRenderer;
    private Animator animator;
    private Effect effect;
    private HealthController healthController;
    private AudioManager audioManager;

    private void Awake()
    {
        //start intro game
        StartCoroutine(gameIntroCoroutine());
    }

    private void Start()
    {
        controllerTransform = GetComponent<Transform>();
        controllerRigidbody = GetComponent<Rigidbody2D>();
        controllerSpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        effect = GameObject.Find("Dash_Effect").GetComponent<Effect>();
        healthController = GetComponent<HealthController>();
        //audioManager = FindObjectOfType<AudioManager>();
        audioManager = GetComponent<AudioManager>();
        
        isSphereable = true;
        isHealable = true;
        isDeath = false;
        isEvadeable = true;
        isDashable = true;
        isAirDash = false;
        isInSkillInterval = false;
        isJavelinThrowable = true;
        isInvincible = false;
        isFreezeInputMovement = false;
        isInputable = true;
        gravityScale = controllerRigidbody.gravityScale;
        xVelocity = 0;

        recoverPosition = controllerTransform.position;
    }

    private void Update()
    {
        if(isGameIntro)
        {
            return;
        }

        updateCharacterState();
        //movement
        Jump();
        //Subsidize
        Evade();
        Dash();
        //attack
        Sphere();
        JavelinThrow();

        Heal();
    }

    private void FixedUpdate()
    {
        if(isGameIntro)
        {
            return;
        }
        Movement();
    }

    #region intro game character
    private IEnumerator gameIntroCoroutine()
    {
        CineController.Instance.shakeDuration = 0.3f;
        isGameIntro = true;
        yield return new WaitForSeconds(gameIntroInterval);

        isGameIntro = false;
    }
    #endregion

    #region update state character
    private void updateCharacterState()
    {
        if(isDeath)
        {
            controllerRigidbody.velocity = Vector2.zero;
        }


        isFalling = controllerRigidbody.velocity.y < 0 ? true : false;
        isFalling = isFalling && !isInSkillInterval && !isJumping;
        animator.SetBool("isFalling", isFalling);

        if(isGround)
        {
            isClimb = false;
            animator.ResetTrigger("Jump");
            animator.ResetTrigger("ClimbJump");
            animator.SetBool("isFalling", false);
        }
    }

    #endregion

    #region getter, setter region
    public void setIsGround(bool isGround)
    {
        this.isGround = isGround;
        animator.SetBool("isGround", isGround);
        //audioManager.playSound("Land");
    }

    public void setIsClimb(bool isClimb)
    {
        this.isClimb = isClimb && !isGround;
        animator.SetBool("isClimb", this.isClimb);
        if(isClimb)
        {
            audioManager.playSound("Climb");
            controllerRigidbody.velocity = Vector2.zero;
            controllerRigidbody.gravityScale = gravityScale/4;
        }
        else  
        {
            controllerRigidbody.gravityScale = gravityScale;
        }
    }

    public bool getIsAirDash()
    {
        return isAirDash;
    }

    #endregion

    public void blackScreen(int input)
    {
        bool isRender = input == 1 ? true : false;
        GameObject.Find("Black_Solid").GetComponent<SpriteRenderer>().enabled = isRender;
    }

    public int getDirection()
    {
        return isFacingLeft == true ? -1 : 1;
    }

    // no deduct health player
    public void setInvincible(int isInvincible)
    {
        this.isInvincible = isInvincible == 0 ? false : true;
    }
}
