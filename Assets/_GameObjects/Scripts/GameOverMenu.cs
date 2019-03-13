using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    //Start: pausar el juego

    public void RestartGame()
    {
        //des-pausar el juego
        SceneManager.LoadScene("Scene1");
        Time.timeScale = 1;
        GameManager.ResetLivesAndPoints();
        gameObject.SetActive(false); //desactivar el menú de pausa (este gameObject)
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        GameManager.ResetLivesAndPoints();
        SceneManager.LoadScene("MenuScene");
    }
}