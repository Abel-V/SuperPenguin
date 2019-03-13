using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject prefabEffect;
    public int health = 50;
    public int touchDamage = 25;
    [SerializeField] float force = 500;

    virtual public void ReceiveDamage(int damage)
    {
        print("AUU");
        health = health - damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
        Instantiate(prefabEffect, transform.position, transform.rotation);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER))
        {
            collision.gameObject.GetComponent<Player>().ReceiveDamage(touchDamage);
            collision.gameObject.GetComponent<Player>().SetImpulse(force);
        }
    }
}
