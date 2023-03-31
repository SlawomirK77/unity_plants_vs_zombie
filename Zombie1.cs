using System;
using UnityEngine;

public class Zombie1 : MonoBehaviour
{
    public GameObject gameObject;
    public float moveSpeed = 1f;
    public float attackTick = 2f;
    public float currentAttackTick = 2f;
    public float damage = 4f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool canMove = true;

    private float _health = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        RefreshZombie();
        rb.velocity = new Vector2(-1f, 0f) * moveSpeed;    

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            float damage = collision.gameObject.GetComponent<Tower1Bullet>().damage;
            _health -= damage;
            if (_health < 1)
            {
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Plant"))
        {
            rb.velocity = new Vector2(0, 0f);
            animator.SetBool("isWalking", false);
            collision.gameObject.GetComponent<Tower1>().LoseHealth(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plant"))
        {
            if (currentAttackTick > 0)
            {
                currentAttackTick -= Time.deltaTime;
            }
            else
            {
                collision.gameObject.GetComponent<Tower1>().LoseHealth(damage);
                currentAttackTick = attackTick;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Plant"))
        {
            rb.velocity = new Vector2(-1f, 0f) * moveSpeed;
            animator.SetBool("isWalking", true);
        }
    }

    private void RefreshZombie()
    {
        _health = 10f;
    }
}