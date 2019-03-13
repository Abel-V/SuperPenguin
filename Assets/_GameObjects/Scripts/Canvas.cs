using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public Text txtScore;
    public Image[] imgLives;
    [SerializeField] GameObject panelPauseMenu;
    [SerializeField] GameObject panelGameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Image imagen in imgLives)
        {
            imagen.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        txtScore.text = GameManager.Points.ToString();
        //Desactivamos todos los corazones
        for (int i = 0; i < GameManager.NUM_MAX_LIVES; i++)
        {
            imgLives[i].enabled = false;
        }
        //Activamos los corazones correspondientes a la vidas que me quedan
        for (int i = 0; i < GameManager.Lives; i++)
        {
            imgLives[i].enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivatePauseMenu();
        }
        if (GameManager.Lives == 0)
        {
            GameManager.Lives = -1; //arreglo para que no entre todo el rato en bucle
            ActivateGameOverMenu();
        }
    }

    public void ActivatePauseMenu()
    {
        panelPauseMenu.SetActive(!panelPauseMenu.activeSelf); //lo contrario de lo que esté (true/false)
        if (panelPauseMenu.activeSelf)
        {
            Time.timeScale = 0; //congelamos el juego
        }
        else
        {
            Time.timeScale = 1;
        }
        // o la forma pro: Time.TimeScale = (panelMenu.activeSelf) ? 0 : 1;
    }

    public void ActivateGameOverMenu()
    {
        panelGameOverMenu.SetActive(true);
        Time.timeScale = 0; //congelamos el juego
    }
}
