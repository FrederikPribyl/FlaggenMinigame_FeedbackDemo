using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Flag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string id;

    Vector2 initPosition;
    Vector2 startPos;
    Camera cam;
    RectTransform rectTransform;
    RectTransform fullscreen_reference;
    Vector2 dragOffset;
    CanvasGroup canvasGroup;

    [HideInInspector] public bool locked;
    public bool animation_enabled;

    private void Start()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        fullscreen_reference = GameObject.FindObjectOfType<FullScreenReference>().GetComponent<RectTransform>();
        locked = false;
        initPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(locked) return;
        startPos = rectTransform.anchoredPosition;
        dragOffset = MousePosToCanvas() - rectTransform.anchoredPosition;
        // Debug.Log("Begin drag");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.8f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // eventData.
        // transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        if (locked) return;
        rectTransform.anchoredPosition = MousePosToCanvas() - dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    Vector2 MousePosToCanvas(){
        RectTransformUtility.ScreenPointToLocalPointInRectangle(fullscreen_reference, Input.mousePosition, cam, out Vector2 localPoint);
        return localPoint;
    }

    public void Lock(){
        locked = true;
        if(animation_enabled)
        GetComponent<Animator>().SetBool("locked", true);
        transform.SetParent(GameManager.Instance.lockedFlagsParent);
        // Vector3 pos = GetComponent<RectTransform>().anchoredPosition3D;
        // GetComponent<RectTransform>().anchoredPosition3D = new Vector3(pos.x, pos.y, 1f);
    }

    public void RestoreStartPosition(){
        rectTransform.anchoredPosition = startPos;
    }

    public void Reset(){
        rectTransform.anchoredPosition = initPosition;
        locked = false;
        GetComponent<Animator>().SetBool("locked", false);
        transform.SetParent(GameManager.Instance.lockedFlagsParent.parent);
    }

}
