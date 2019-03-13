using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public AudioClip collectSound;
    public GameObject prefabEffect;

    
    public virtual void DoAction()
    {

    }

    public void RunEffect()
    {
        if(prefabEffect != null) {
            Instantiate(prefabEffect, transform.position, transform.rotation);
        }
    }

    public virtual void Kill()
    {
        if (prefabEffect != null) {
            RunEffect();
        }
    }

    public void PlaySound() { }
}
