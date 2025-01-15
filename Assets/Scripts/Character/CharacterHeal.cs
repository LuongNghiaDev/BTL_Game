using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Character : IHeal
{

    [Header("Hurt Section")]
    [SerializeField] GameObject hitParticle;
    [SerializeField] Color invulnerableColor;
    [SerializeField] float invulnerableInterval;
    [SerializeField] float freezeSenceInterval;

    [Header("Healing Section")]
    [SerializeField] GameObject healingParticle;
    [SerializeField] int healRecover;
    [SerializeField] float healingInterval;
    [SerializeField] float healingCooldownInterval;
    private bool isHealable;


    #region heal character
    public void Heal()
    {
        if (Input.GetKeyDown(KeyCode.H) && !isInSkillInterval && isHealable && isInputable && !isClimb)
        {
            StartCoroutine(healCoroutine());
        }
    }

    private IEnumerator healCoroutine()
    {
        isHealable = false;
        isInSkillInterval = true;
        freezeGravity();
        animator.SetTrigger("Healing");

        yield return new WaitForSeconds(healingInterval);
        animator.ResetTrigger("Healing");
        isInSkillInterval = false;
        unFreezeGravity();
        healthController.healing(healRecover);




        // Health bar
        GameObject healthBar = GameObject.Find("hp_" + healthController.getHealthPoint());
        healthBar.GetComponent<Animator>().SetTrigger("Health");

        if (healthController.getHealthPoint() == 2)
        {
            GameObject.Find("Vintage_Low_Health").GetComponent<SpriteRenderer>().enabled = false;
        }

        yield return new WaitForSeconds(healingCooldownInterval);
        isHealable = true;
    }

    public void heallingEvent()
    {
        // create an instance of blood leak out
        Vector3 vectorTransform = Vector3.up * 1;
        Instantiate(healingParticle, controllerTransform.position + vectorTransform, controllerTransform.rotation);
    }
    #endregion

    #region hurt character
    public void takeDamage(int damage)
    {
        if (isInvincible)
        {
            return;
        }

        // Health bar
        GameObject healthBar = GameObject.Find("hp_" + healthController.getHealthPoint());
        healthBar.GetComponent<Animator>().SetTrigger("Break");

        if (healthController.getHealthPoint() == 2)
        {
            GameObject.Find("Vintage_Low_Health").GetComponent<SpriteRenderer>().enabled = true;
        }

        audioManager.playSound("TakeDamage");
        healthController.takeDamage(damage);
        if (healthController.getHealthPoint() <= 0 && !isDeath)
        {
            StartCoroutine(dieCoroutine());
            CineController.Instance.shakeDuration = deathEffectInterval;
            CineController.Instance.ShakeTrigger();
            //StartCoroutine(cameraShake.startShake(deathEffectInterval));
        }
        else
        {
            StartCoroutine(stunCoroutine());
            StartCoroutine(invulnerableCoroutine());
        }
    }

    private IEnumerator stunCoroutine()
    {
        freezeGravity();
        animator.SetTrigger("Stun");


        // create an instance of particle
        Instantiate(hitParticle, controllerTransform.position, controllerTransform.rotation);

        isInputable = false;

        yield return new WaitForSeconds(0.01f * freezeSenceInterval);

        isInputable = true;
        unFreezeGravity();

        yield return new WaitForSeconds(1f);
        animator.ResetTrigger("Stun");
    }


    private IEnumerator dieCoroutine()
    {
        if (PlayerPrefs.GetInt("PlayDauTruong") == 1)
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
            PlayerPrefs.Save();
        }
        freezeGravity();
        isDeath = true;
        audioManager.playSound("DeathYell");
        yield return new WaitForSeconds(0.1f);
        audioManager.playSound("DeathSound");
        animator.SetTrigger("Death");
        isInvincible = true;
        isInputable = false;

        yield return new WaitForSeconds(deathEffectInterval);

        // health recover
        healthController.healing(5);
        GameObject.Find("Vintage_Low_Health").GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 1; i <= 5; i++)
        {
            // Health bar
            GameObject healthBar = GameObject.Find("hp_" + i);
            healthBar.GetComponent<Animator>().SetTrigger("Health");
        }

        isInputable = true;
        controllerTransform.position = recoverPosition;
        animator.ResetTrigger("Death");
        unFreezeGravity();
        isDeath = false;
        isInvincible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //StartCoroutine(gameIntroCoroutine());
    }

    private IEnumerator invulnerableCoroutine()
    {
        isInvincible = true;

        // collor flash color
        controllerSpriteRenderer.color = invulnerableColor;
        yield return new WaitForSeconds(invulnerableInterval);
        controllerSpriteRenderer.color = Color.white;

        isInvincible = false;
    }
    #endregion
}
