using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveKnightController : MonoBehaviour
{

    [SerializeField] float activeDistance;

    [Header("Attack")]
    [SerializeField] float attackDashRange;
    [SerializeField] float attackDashVelocity;

    [Header("Stomp")]
    [SerializeField] float stompHeight;
    [SerializeField] float stompFallVelocity;

    [Header("Evade")]
    [SerializeField] float evadeRange;
    [SerializeField] float evadeVelocity;

    [Header("Cast Bullet Point")]
    [SerializeField] Transform castPoint;
    [SerializeField] GameObject orb;

    [SerializeField]
    private Transform enemyTransform;
    private Transform hiveKnightTransform;
    private Rigidbody2D hiveKnightRigidbody;
    private Animator animator;

    private float gravityScale;

    private bool isFlipped;
    private bool isSkill;
    private bool isSleep;

    [SerializeField]
    private CapsuleCollider2D colliderSupport;

    private void Start()
    {

        hiveKnightTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        hiveKnightRigidbody = GetComponent<Rigidbody2D>();

        isFlipped = false;
        isSkill = false;
        isSleep = true;

        gravityScale = hiveKnightRigidbody.gravityScale;
    }

    private void Update()
    {
        wake();
    }

    private void wake()
    {
        if (Vector2.Distance(enemyTransform.position, hiveKnightTransform.position) <= activeDistance && isSleep)
        {
            animator.SetTrigger("Wake");
            isSleep = false;
        }
    }

    public void lookAtPlayer()
    {
        Vector3 flipped = hiveKnightTransform.localScale;
        flipped.z *= -1f;

        if (hiveKnightTransform.position.x > enemyTransform.position.x && isFlipped)
        {
            hiveKnightTransform.localScale = flipped;
            hiveKnightTransform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (hiveKnightTransform.position.x < enemyTransform.position.x && !isFlipped)
        {
            hiveKnightTransform.localScale = flipped;
            hiveKnightTransform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void teleportOutEvent()
    {
        int randSkill = Random.Range(0, 2);

        if (randSkill == 0)
        {
            stompControl();
        }
        else
        {
            castControl();
        }
    }

    //attack slash section
    public void attackDashEvent()
    {
        int direction = isFlipped == true ? 1 : -1;
        Vector2 newPosition = Vector2.MoveTowards(hiveKnightRigidbody.position, hiveKnightRigidbody.position + Vector2.right * attackDashRange * direction,
            attackDashVelocity * Time.fixedDeltaTime);
        hiveKnightRigidbody.MovePosition(newPosition);
    }

    // stomp section
    private void stompControl()
    {
        animator.SetTrigger("Stomp");
        hiveKnightRigidbody.position = enemyTransform.position + Vector3.up * stompHeight;
    }

    public void stompFallEvent()
    {
        hiveKnightRigidbody.velocity = Vector2.down * stompFallVelocity;
    }

    // Evade section
    public void evadeControl()
    {
        int direction = isFlipped == true ? -1 : 1;
        Vector2 newPosition = Vector2.MoveTowards(hiveKnightRigidbody.position, hiveKnightRigidbody.position + Vector2.right * evadeRange * direction, evadeVelocity * Time.fixedDeltaTime);
        hiveKnightRigidbody.MovePosition(newPosition);

        int randSkill = Random.Range(0, 2);
        if (randSkill == 0)
        {
            animator.SetTrigger("DashTeleport");
        }
        else
        {
            animator.SetTrigger("DashRecover");
        }
    }


    // cast section
    private void castControl()
    {
        animator.SetTrigger("Cast");
    }

    public void castOrbEvent()
    {
        // create an instance of orb
        //Instantiate(orb, castPoint.position, castPoint.rotation);
    }

    public void freezeGravityEvent()
    {
        hiveKnightRigidbody.velocity = Vector2.zero;
        hiveKnightRigidbody.gravityScale = 0;
    }

    public void unFreezeGravityEvent()
    {
        hiveKnightRigidbody.gravityScale = gravityScale;
    }

    #region setter, getter section
    public void setIsSkill()
    {
        this.isSkill = true;
    }

    public void resetIsSkill()
    {
        this.isSkill = false;
    }

    public bool getIsSkill()
    {
        return isSkill;
    }

    #endregion


}
