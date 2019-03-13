using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Vector2 startposition;
    public Transform targetPosition;
    private float pct; //porcentaje de avance
    public float speed = 0.5f;

    /*
    public float distance;
    private float offset = 0;
    public float speed;
    */

    private void Start()
    {
        startposition = transform.position;
    }

    void Update()
    {
        pct = pct + Time.deltaTime * speed;
        transform.position = Vector2.Lerp(startposition, targetPosition.position, pct);
        if (pct > 1 || pct < 0) {
            speed = speed * -1;
            //para que no se pase de rango y por tanto no se encasquille:
            if (pct > 1) { pct = 1; } else if (pct < 0) { pct = 0; }
        }

        /* //MOVIMIENTO HORIZONTAL constante
        offset += Time.deltaTime * speed;
        transform.Translate(new Vector2(Time.deltaTime * speed, 0));
        if (offset >= distance || offset <= 0) {
            speed = speed * -1;
        }
        */
    }
}
