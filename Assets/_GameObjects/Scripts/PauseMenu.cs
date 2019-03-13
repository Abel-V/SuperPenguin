using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Start: pausar el juego

    public void ContinueGame()
    {
        //des-pausar el juego
        Time.timeScale = 1;
        gameObject.SetActive(false); //desactivar el menú de pausa (este gameObject)
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
}