using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meta1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StoreConfiguration();
        SceneManager.LoadScene("Scene2");
    }


    private void StoreConfiguration()
    {
        PlayerPrefs.SetInt("punt1", GameManager.Points);
        GameManager.Points = 0; //para que se reinicien en la siguiente escena
        PlayerPrefs.Save();
    }
}
