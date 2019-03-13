using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemy : Enemy
{
    [SerializeField] Transform endPos;
    Vector3 initPos;
    [SerializeField] float speed;
    float pct = 0;
    private void Awake()
    {
        initPos = transform.position;
    }

    void Update()
    {
        pct = pct + Time.deltaTime * speed;
        if (pct >= 1 || pct <= 0)
        {
            speed = speed * -1;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            //para que no se pase de rango y por tanto no se encasquille:
            if (pct > 1) { pct = 1; } else if (pct < 0) { pct = 0; } 
        }
        transform.position = Vector2.Lerp(initPos, endPos.position, pct);

    }

    override public void ReceiveDamage(int i)
    {
        base.ReceiveDamage(i);
        GetComponentInChildren<Slider>().value = health;
    }
}