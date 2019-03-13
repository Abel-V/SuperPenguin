using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public float scaleSpeed = 1;
    private float scale = 1;
    public float upperScaleLimit = 2;
    public float bottomScaleLimit = 1;

    // Update is called once per frame
    void Update()
    {
        scale = scale + scaleSpeed * Time.deltaTime;
        if (scale > upperScaleLimit || scale < bottomScaleLimit) {
            scaleSpeed = scaleSpeed * -1;
        } 
        transform.localScale = new Vector2(scale, scale);
    }
}
