using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle modoDiosToggle;
    public Slider sliderSonido;
    private const string PPK_VOLUMEN = "volumen";
    private const string PPK_GOD_MODE = "modoDios";

    // Start is called before the first frame update
    void Start()
    {
        int modoDios;
        int volumen = 100;
        if (PlayerPrefs.HasKey(PPK_VOLUMEN)) {
            volumen = PlayerPrefs.GetInt(PPK_VOLUMEN);
        }
        modoDios = PlayerPrefs.GetInt(PPK_GOD_MODE, 0);
        sliderSonido.value = volumen;
        modoDiosToggle.isOn = modoDios == 0 ? true : false;
    }
    
    void OnDisable()
    {
        StoreConfiguration();
    }

    private void StoreConfiguration()
    {
        //print(modoDiosToggle.isOn);
        //print(sliderSonido.value.ToString());

        int modoDios = modoDiosToggle.isOn ? 0 : 1;
        PlayerPrefs.SetInt(PPK_GOD_MODE, modoDios);
        PlayerPrefs.SetInt(PPK_VOLUMEN, (int)sliderSonido.value);
        PlayerPrefs.Save();
    }
}
