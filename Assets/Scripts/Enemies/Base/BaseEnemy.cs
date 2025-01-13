using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    //active
    [SerializeField] protected float activeDistance;

    protected Transform playerTransform;
    protected Rigidbody2D objRigidbody;
    protected Animator animator;
    protected HealthController healthController;

    protected float gravityScale;

    protected bool isFlipped;
    protected bool isSkill;
    protected bool isSleep;
    protected bool isDeath;

    [SerializeField]
    private bool openMusicAttack;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        healthController = GetComponent<HealthController>();
        objRigidbody = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        isFlipped = false;
        isSkill = false;
        isSleep = true;
        isDeath = false;

        gravityScale = objRigidbody.gravityScale;

        
    }

    public void wake()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) <= activeDistance && isSleep)
        {
            animator.SetTrigger("Wake");
            isSleep = false;

            if(openMusicAttack == true)
            {
                MusicController musicController = FindObjectOfType<MusicController>();
                StartCoroutine(musicController.fightPrepareCoroutine());
            }
        }
    }

    public void lookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > playerTransform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerTransform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void freezeGravityEvent()
    {
        objRigidbody.velocity = Vector2.zero;
        objRigidbody.gravityScale = 0;
    }

    public void unFreezeGravityEvent()
    {
        objRigidbody.gravityScale = gravityScale;
    }

    public void DeathControl()
    {
        if (healthController.getHealthPoint() <= 0 && !isDeath)
        {
            animator.SetTrigger("Death");
            OnDie();
            if (openMusicAttack == true)
            {
                MusicController musicController = FindObjectOfType<MusicController>();
                musicController.fightEndCoroutine();
            }
            isDeath = true;

        }
    }

    public virtual void OnDie()
    {
        Debug.Log("OnDie");
    }

    public void destroyGameObject()
    {
        Destroy(gameObject);
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
