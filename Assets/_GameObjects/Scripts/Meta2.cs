using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta2 : MonoBehaviour
{
    public GameObject gameFinishedPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StoreConfiguration();
        gameFinishedPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void StoreConfiguration()
    {
        PlayerPrefs.SetInt("punt2", GameManager.Points);
        GameManager.Points = 0; //para que se reinicien en la siguiente escena
        PlayerPrefs.Save();
    }
}
