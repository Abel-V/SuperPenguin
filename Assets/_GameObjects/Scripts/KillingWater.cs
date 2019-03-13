using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingWater : MonoBehaviour
{
    public int damage = 5;
    private GameObject player;
    //public GameObject canvasPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            InvokeRepeating("Ahogar", 0, 1);
            //canvasPanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CancelInvoke();
            //canvasPanel.SetActive(false);
        }
    }

    private void Ahogar()
    {
        player.GetComponent<Player>().ReceiveDamage(damage);
    }
}
