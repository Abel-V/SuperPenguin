using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPenguin : Enemy
{
    //enum Estado { Idle, Attack };
    //private Estado estado = Estado.Idle;

    public GameObject snowballPrefab;
    public Transform spawnPoint;
    public Transform ejeRotacion;
    public float fuerzaDisparo = 200;
    public float cadenciaDisparo = 3;
    public Transform transformPlayer;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("Disparar", 5, cadenciaDisparo); 
        //que dispare todo el rato, porque hay incompatibilidad haciendolo con una esfera grande 
        //que valga de trigger de detección, por el sistema de trigger del player (se destruye)
    }

    private void Disparar() //CONVERTIR EN IENUMERATOR Y USAR WAITFORSECONDS PARA MAS REALISMO
    {
        animator.SetTrigger("Firing");
        GameObject bala = Instantiate(snowballPrefab, spawnPoint.position, spawnPoint.rotation);
        bala.GetComponent<Rigidbody2D>().AddForce(spawnPoint.transform.right * fuerzaDisparo);

    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //estado = Estado.Attack;
            InvokeRepeating("Disparar", 0, 1 / cadenciaDisparo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //estado = Estado.Idle;
            CancelInvoke();
        }
    }
    */

    private void Update()
    {
        if (gameObject.transform.position.x < transformPlayer.position.x)
        {
            transform.rotation = Quaternion.Euler(Vector2.zero);
        }
        else if (gameObject.transform.position.x > transformPlayer.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector2(0, 180));
        }

        ejeRotacion.LookAt(transformPlayer.position + new Vector3(0, 0.5f, 0)); //NO VA BIEN EN 2D
        ejeRotacion.Rotate(0, -90, 0); //CORRECCION
    }
}
