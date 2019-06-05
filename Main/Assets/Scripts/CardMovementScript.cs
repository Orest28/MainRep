using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMovementScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler, IPointerExitHandler
{
    Camera MainCamera;
    public Transform DefaultParent, DefaultTempCardParent;
    GameObject TempCardGO;
    public static bool flag;
    public static Sprite ZoomLogo;
    public Image temp;
    public ZoomCardFilling zoomCardFilling;
    public bool IsDraggable;
    public GameManagerSrc GameManager;
    public static int id;

    void Start()
    {
        zoomCardFilling = new ZoomCardFilling();
    }

    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        TempCardGO = GameObject.Find("TempCardGo");     // створюєм гаме обджект і присвоюєм йому карту яка належить канвасу є замежами поля ну і вона є тимчасовою
        GameManager = FindObjectOfType<GameManagerSrc>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DefaultParent = DefaultTempCardParent = transform.parent;

        IsDraggable = DefaultParent.GetComponent<DropPlaceScript>().Type == FieldType.SELF_HAND;

        if (!IsDraggable)
            return;

        TempCardGO.transform.SetParent(DefaultParent);
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());      // временная карта на тому місці де ми підняли нашу карту "нє"

        transform.SetParent(DefaultParent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDraggable)
            return;

        Vector3 newPos = eventData.position;
       //  newPos.z = 0; ----- delete in next movie
        transform.position = newPos;

        if (TempCardGO.transform.parent != DefaultTempCardParent)
            TempCardGO.transform.SetParent(DefaultTempCardParent);

        CheckPosition();            ///  метод для того щоб переміщати уявну карту коли ми переміщаєм норм карту

        flag = true;
        temp = transform.GetChild(0).GetComponentInChildren<Image>();
        ZoomLogo = temp.sprite;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsDraggable)
            return;

        transform.SetParent(DefaultParent);

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());      //      переставляєм карту 

        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGO.transform.localPosition = new Vector3(1218, -290);     
    }

    void CheckPosition()        ///  метод для того щоб переміщати уявну карту коли ми переміщаєм норм карту
    {
        int newIndex = DefaultTempCardParent.childCount;

        for (int i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            if (transform.position.x < DefaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;
                if (TempCardGO.transform.GetSiblingIndex() < newIndex)
                    newIndex--;

                break;
            } 
        }
        TempCardGO.transform.SetSiblingIndex(newIndex);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        id = GetComponent<CardGiven>().ID;
        flag = true;
        temp = transform.GetChild(0).GetComponentInChildren<Image>();
        ZoomLogo = temp.sprite;
        Debug.Log(ZoomLogo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        flag = false;
    }
    
}
