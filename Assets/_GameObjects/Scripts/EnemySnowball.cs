using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnowball : MonoBehaviour
{
    public int damage = 15;
    public GameObject spriteSnowball;
    public GameObject spriteCrashed;
    public Rigidbody2D rb;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER))
        {
            //print("BOLA");
            collision.gameObject.GetComponent<Player>().ReceiveDamage(damage);
            collision.gameObject.GetComponent<Player>().SetImpulse(200);
            AutoDestroy();
        }
        else
        {
            if (collision.gameObject.tag != "EnemySnowball" && collision.gameObject.tag != "Snowball")
            {
                spriteSnowball.SetActive(false);
                spriteCrashed.SetActive(true);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                Invoke("AutoDestroy", 0.5f);
            }
        }
    }

    private void AutoDestroy()
    {
        Destroy(this.gameObject);
    }
}