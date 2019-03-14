using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{

    private void Start()
    {
        Invoke("Destruir", 2); //para que le de tiempo al sonido del prefab a reproducirse
    }

    public void Destruir()
    {
        Destroy(this.gameObject);
    }
    
}
