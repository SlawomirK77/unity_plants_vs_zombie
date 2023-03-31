using System;
using UnityEngine;

public class Tower1Bullet : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float damage = 1f;
    public GameObject bulletHitEffect;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1f, 0f) * moveSpeed;
        rb.rotation = 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie") || collision.gameObject.CompareTag("BulletWall"))
        {
            GameObject effect = Instantiate(bulletHitEffect, transform.position, transform.rotation);
            Destroy(effect, 2f);
            Destroy(transform.gameObject);
        }
    }
}