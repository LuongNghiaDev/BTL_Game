using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceSentryCtrl : MonoBehaviour
{
    [SerializeField] protected float activeDistance;

    [SerializeField] float evadeRange;
    [SerializeField] float takeDamageForce;
    //death
    [SerializeField] float airDeathInterval;
    [SerializeField] float landDeathInterval;

    [Header("Shoot Section")]
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject projectile;

    private AudioManager audioManager;

    protected Transform playerTransform;
    protected Rigidbody2D objRigidbody;
    protected Animator animator;
    protected HealthController healthController;

    protected float gravityScale;
    public bool isGround = true;

    public bool isFlipped;
    protected bool isSkill;
    protected bool isSleep;
    protected bool isDeath;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        healthController = GetComponent<HealthController>();
        objRigidbody = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = GetComponent<AudioManager>();

        isFlipped = false;
        isSkill = false;
        isSleep = true;
        isDeath = false;

        gravityScale = objRigidbody.gravityScale;
    }

    // Update is called once per frame
    private void Update()
    {
        wake();
        DeathControl();
    }

    public void wake()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) <= activeDistance && isSleep)
        {
            animator.SetTrigger("Wake");
            isSleep = false;

            MusicController musicController = FindObjectOfType<MusicController>();
            StartCoroutine(musicController.fightPrepareCoroutine());
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

            MusicController musicController = FindObjectOfType<MusicController>();
            musicController.fightEndCoroutine();
            isDeath = true;
            objRigidbody.gravityScale = 5;
            gameObject.SetActive(false);
        }
    }

    public void destroyGameObject()
    {
        Destroy(gameObject);
    }

    #region event region
    public void evadeEvent()
    {
        int randSound = Random.Range(1, 5);
        int direction = isFlipped == true ? -1 : 1;
        objRigidbody.AddForce(new Vector2(direction * evadeRange, 0));

        audioManager.playSound("AttackYell" + randSound);
    }

    public void shootEvent()
    {
        GameObject bullet = (GameObject)Instantiate(projectile, shotPoint.position, shotPoint.rotation);
        bullet.transform.Rotate(0f, 0f, Mathf.Atan2(shotPoint.position.y, shotPoint.position.x) * Mathf.Rad2Deg);
    }

    /*private void Shooting()
    {

        float distance = Mathf.Abs(transform.position.x - playerTransform.position.x);

        bullet.transform.Rotate(0f, 0f, Mathf.Atan2(shotPoint.position.y, shotPoint.position.x) * Mathf.Rad2Deg);
        Rigidbody2D orbRigidbody = bullet.GetComponent<Rigidbody2D>();
        Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(orbRigidbody.position, target, 10 * Time.fixedDeltaTime);
        orbRigidbody.MovePosition(newPosition);
    }*/

    public void slashEvent()
    {
        int randSound = Random.Range(1, 5);
        audioManager.playSound("AttackYell" + randSound);
        audioManager.playSound("Slash");
    }
    #endregion

    #region setter, getter section
    public void setIsShield()
    {
        this.isSkill = true;
    }

    public void resetIsShield()
    {
        this.isSkill = false;
    }

    public bool getIsShield()
    {
        return isSkill;
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grass"))
        {
            isGround = true;
        }
        if (collision.gameObject.CompareTag("Acid"))
        {
            healthController.takeDamage(100);
        }
    }
}
