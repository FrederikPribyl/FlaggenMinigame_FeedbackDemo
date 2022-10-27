using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IDropHandler
{
    public string id;
    public string title;
    public TMP_Text label_TMP;

    private void Start() {
        label_TMP.text = title;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.TryGetComponent<Flag>(out Flag flag)){
            if(flag.id.Equals(this.id)){
                flag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                flag.Lock();
                GetComponentInChildren<ParticleSystem>().Play();
            } 
            else flag.RestoreStartPosition();
        }
    }
}
