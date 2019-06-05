using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZoomCardInShop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool flag;
    public static Sprite ZoomLogo;
    public Image temp;
    public static int ID;
    public ZoomCardFillingForShop zoomCardFillingForShop;

    // Start is called before the first frame update
    void Start()
    {
        zoomCardFillingForShop = new ZoomCardFillingForShop();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ID = GetComponent<CardInfoSrc>().ID;
        flag = true;
        temp = transform.GetChild(0).GetComponentInChildren<Image>();
        ZoomLogo = temp.sprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        flag = false;
    }
}
