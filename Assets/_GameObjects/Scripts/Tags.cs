using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags  //no tiene que ser MonoBehaviour->objetos de la escena de juego
{
    public static string ITEM = "Item";
    public static string PLAYER = "Player";
    public static string ENEMY = "Enemy";
    public static string MOBILEPLATFORM = "MobilePlatform";
    //lo hacemos static para que no haga falta instanciarlo luego
    //queríamos que fuese una constante pero no dejaba, al poner static
}
