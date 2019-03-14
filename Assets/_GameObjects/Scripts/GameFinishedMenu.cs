using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFinishedMenu : MonoBehaviour
{
    private int punt1;
    private int punt2;
    private int maxPunt1;
    private int maxPunt2;
    public Text punt1Text;
    public Text punt2Text;


    private void Start()
    {
        punt1 = PlayerPrefs.GetInt("punt1", 0);
        punt2 = PlayerPrefs.GetInt("punt2", 0);
        maxPunt1 = PlayerPrefs.GetInt("maxPunt1", 0);
        maxPunt2 = PlayerPrefs.GetInt("maxPunt2", 0);
        ShowPoints();
        StoreMaxPoints();
    }

    public void RestartGame()
    {
        //des-pausar el juego
        SceneManager.LoadScene("Scene1");
        Time.timeScale = 1;
        GameManager.ResetLivesAndPoints();
        gameObject.SetActive(false); //desactivar el menú (este gameObject)
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        GameManager.ResetLivesAndPoints();
        SceneManager.LoadScene("MenuScene");
    }

    private void ShowPoints()
    {
        punt1Text.text = punt1.ToString();
        punt2Text.text = punt2.ToString();
    }

    private void StoreMaxPoints()
    {
        if(punt1 > maxPunt1)
        {
            PlayerPrefs.SetInt("maxPunt1", punt1);
        }
        if (punt2 > maxPunt2)
        {
            PlayerPrefs.SetInt("maxPunt2", punt2);
        }

        GameManager.Points = 0; //para que se reinicien en la siguiente escena
        PlayerPrefs.Save();
    }
}