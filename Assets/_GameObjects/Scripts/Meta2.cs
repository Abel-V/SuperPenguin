using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta2 : MonoBehaviour
{
    public GameObject gameFinishedPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("heeeey");
        gameFinishedPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
