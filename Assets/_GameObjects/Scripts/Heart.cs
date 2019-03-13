using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item
{
    public const int NUM_VIDAS = 1;

    public override void Kill()
    {
        base.Kill();
        Destroy(this.gameObject);
    }

    public override void DoAction()
    {
        base.DoAction();
        if (GameManager.Lives < GameManager.NUM_MAX_LIVES)
        {
            GameManager.AddLife(NUM_VIDAS);
            Kill();
        }
    }
}
