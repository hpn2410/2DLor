using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BossAI : MonoBehaviour
{
    public float chaseRadius = 15f;
    public float attackRadius = 5f;
    public float shieldCooldown = 5f;
    public float attack1Cooldown = 5f;
    public float attack2Cooldown = 7f;
    public Transform player;
    private Animator animator;
    public bool isShielding = false;
    private bool canAttack = true;
    private bool isMoving = false;
    private float maxHealth = 250f;
    private float currentHealth;
    private float bossDamage = 30f;

    private enum State { Idle, Chase, Attack1, Attack2, Shield }
    private State currentState = State.Idle;

    public Action<float, float> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StartCoroutine(StateMachine());
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= attackRadius && canAttack)
        {
            int attackType = Random.Range(1, 4); // Random attack state
            if (attackType == 1)
            {
                currentState = State.Attack1;
            }
            else if (attackType == 2)
            {
                currentState = State.Attack2;
            }
            else
            {
                currentState = State.Shield;
            }
        }
        else if (distance <= chaseRadius)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Idle;
        }
    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Chase:
                    Chase();
                    break;
                case State.Attack1:
                    yield return Attack1();
                    break;
                case State.Attack2:
                    yield return Attack2();
                    break;
                case State.Shield:
                    yield return Shield();
                    break;
            }
            yield return null;
        }
    }

    private void Idle()
    {
        isMoving = false;
        animator.SetFloat("x", 0);
        animator.SetFloat("y", 0);
        animator.SetBool("isMoving", isMoving);
    }

    private void Chase()
    {
        isMoving = true;
        animator.SetBool("isMoving", isMoving);

        Vector3 direction = (player.position - transform.position).normalized;
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.z);

        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * 2f);
    }

    private IEnumerator Attack1()
    {
        canAttack = false;
        isMoving = false;
        animator.SetBool("Attack1", true);
        animator.SetBool("Attack2", false);
        animator.SetBool("Shield", false);
        animator.SetBool("isMoving", isMoving);

        yield return new WaitForSeconds(1.5f); // animationTime
        animator.SetBool("Attack1", false);
        canAttack = true;
        SetNextState();
        yield return new WaitForSeconds(attack1Cooldown);
    }

    private IEnumerator Attack2()
    {
        canAttack = false;
        isMoving = false;
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", true);
        animator.SetBool("Shield", false);
        animator.SetBool("isMoving", isMoving);

        yield return new WaitForSeconds(1.5f); // wait for animation
        animator.SetBool("Attack2", false);
        canAttack = true;
        SetNextState();
        yield return new WaitForSeconds(attack2Cooldown);
    }

    private IEnumerator Shield()
    {
        isShielding = true;
        isMoving = false;
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Shield", true);
        animator.SetBool("isMoving", isMoving);

        yield return new WaitForSeconds(1.5f); // Shield time
        animator.SetBool("Shield", false);
        isShielding = false;
        canAttack = true;
        SetNextState();
        yield return new WaitForSeconds(shieldCooldown);
    }

    private void SetNextState()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= chaseRadius)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Idle;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isShielding)
        {
            currentHealth -= damage;
            Debug.Log("Enemy health: " + currentHealth.ToString());
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            DetectDeath();
        }
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            playerStats.TakeDamage(bossDamage);
        }
    }
}