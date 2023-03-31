using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject bullet;
    public ContactFilter2D contactFilter;
    public float maxHealth = 50;
    public float bulletTick = 0.5f;
    public float currentBulletTick = 0.5f;
    private bool _isShooting = false;
    private float health;
    private GameObject bulletWall;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletWall = GameObject.FindWithTag("BulletWall");
        SetHealth();
    }

    private void Update()
    {
        if (currentBulletTick > 0)
        {
            currentBulletTick -= Time.deltaTime;
        }
        else
        {
            Shoot();
            currentBulletTick = bulletTick;
        }
    }

    private void FixedUpdate()
    {
        CheckZombieOnLane();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            TakeHit(collision.gameObject.GetComponent<Zombie1>().damage);
        }
    }

    private void Shoot()
    {
        if (_isShooting)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);   
        }
    }

    private void CheckZombieOnLane()
    {
        _isShooting = rb.Cast(Vector2.right, contactFilter, castCollisions, Vector2.Distance(transform.position, bulletWall.transform.position)) > 0;
    }

    private void SetHealth()
    {
        health = maxHealth;
    }

    private void TakeHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            Destroy(transform.gameObject);
        }
    }

    public void LoseHealth(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            Destroy(transform.gameObject);
        }
    }
}