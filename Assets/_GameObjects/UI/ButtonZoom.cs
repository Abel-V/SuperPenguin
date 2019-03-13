using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonZoom : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float maxScale;
    [SerializeField] float scaleSpeed;
    RectTransform rt;

    //generado automáticamente al hacer click derecho sobre ISelectHandler cuando estaba en rojo
    public void OnSelect(BaseEventData eventData)
    {
        //StartCoroutine("ZoomIn"); //añadido
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //ZoomOut();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine("ZoomIn"); //no me gusta
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ZoomOut();
    }



    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    /*
    public void StartZoomIn()
    {
        
    }

    public void StartZoomOut()
    {

    }
    */
    IEnumerator ZoomIn()
    {
        /*
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button"); 
        foreach (GameObject go in buttons) {
            go.GetComponent<ButtonZoom>().ZoomOut(); //primero ponemos a 1 la escala de todos los botones 
        }
        */
        while ( rt.localScale.x < maxScale) {
            rt.localScale *= 1 + Time.deltaTime * scaleSpeed;
            yield return null;
        }
    }

    private void ZoomOut()
    {
        StopAllCoroutines();
        //rt.localScale *= 1 - Time.deltaTime * scaleSpeed;
        rt.localScale = Vector3.one;
    }
}
