using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public int damage = 20;
    public GameObject spriteSnowball;
    public GameObject spriteCrashed;
    public Rigidbody2D rb;

    private void Start()
    {
        Invoke("AutoDestroy", 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.ENEMY))
        {
            //print("BOLA");
            collision.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
            Destroy(this.gameObject);
        }
        else
        {
            spriteSnowball.SetActive(false);
            spriteCrashed.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void AutoDestroy()
    {
        Destroy(this.gameObject);
    }
}
