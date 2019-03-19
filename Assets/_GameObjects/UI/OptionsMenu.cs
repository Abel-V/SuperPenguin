using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    //public Toggle modoDiosToggle;
    //public Slider sliderSonido;
    //private const string PPK_VOLUMEN = "volumen";
    //private const string PPK_GOD_MODE = "modoDios";

    private int maxPunt1;
    private int maxPunt2;
    public Text punt1Text;
    public Text punt2Text;

    // Start is called before the first frame update
    void Start()
    {
        int modoDios;
        //int volumen = 100;
        /*
        if (PlayerPrefs.HasKey(PPK_VOLUMEN)) {
            volumen = PlayerPrefs.GetInt(PPK_VOLUMEN);
        }*/
       // modoDios = PlayerPrefs.GetInt(PPK_GOD_MODE, 1); //segunda posicion: valor por defecto. (1 es false)
        //sliderSonido.value = volumen;
        //modoDiosToggle.isOn = modoDios == 0 ? true : false;

        maxPunt1 = PlayerPrefs.GetInt("maxPunt1", 0);
        maxPunt2 = PlayerPrefs.GetInt("maxPunt2", 0);
        punt1Text.text = maxPunt1.ToString();
        punt2Text.text = maxPunt2.ToString();
    }
    
    void OnDisable()
    {
        StoreConfiguration();
    }

    private void StoreConfiguration()
    {
        //print(modoDiosToggle.isOn);
        //print(sliderSonido.value.ToString());

        //int modoDios = modoDiosToggle.isOn ? 0 : 1;
        //PlayerPrefs.SetInt(PPK_GOD_MODE, modoDios);
        //PlayerPrefs.SetInt(PPK_VOLUMEN, (int)sliderSonido.value);
        PlayerPrefs.Save();
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("Scene2");
    }
}
