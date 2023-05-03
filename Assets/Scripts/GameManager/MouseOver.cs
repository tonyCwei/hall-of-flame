using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public RectTransform torchL;
    public RectTransform torchR;
    public float shiftAmount;
    RectTransform gameObj;
    float x, y;
    void Start()
    {
        gameObj = GetComponent<RectTransform>();
        x = gameObj.position.x;
        y = gameObj.position.y;
        torchL.gameObject.SetActive(false);
        torchR.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        torchL.gameObject.SetActive(true);
        torchR.gameObject.SetActive(true);
        torchL.position = new Vector2(x - shiftAmount, y);
        torchR.position = new Vector2(x + shiftAmount, y);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //torchL.gameObject.SetActive(false);
        //torchR.gameObject.SetActive(false);
    }
}
