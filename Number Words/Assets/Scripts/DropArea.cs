using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IDragHandler
{

    [SerializeField] Transform ChildSum;
    [SerializeField] Vector3 AnswerCardPosition;
    [SerializeField] bool IsDragging = false;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if ( !eventData.dragging) return;
        if ( ChildSum ) return;
        if ( !GameManager.Instance.GetPickUpAnswerCard ) return;

        IsDragging = eventData.dragging;
        GameManager.Instance.GetPickUpAnswerCard.transform.SetParent(transform);
        GameManager.Instance.GetPickUpAnswerCard.transform.localPosition = AnswerCardPosition;
        GameManager.Instance.GetPickUpAnswerCard.GetComponent<SelectArea>().UpdateDrag(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsDragging)
        {
            // meshRenderer.enabled = false;
            IsDragging = false;
            GameManager.Instance.GetPickUpAnswerCard.GetComponent<SelectArea>().UpdateDrag(true);
            GameManager.Instance.GetPickUpAnswerCard.GetComponent<SelectArea>().ResetParent();
        }     
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if ( ChildSum ) return;
        if ( !GameManager.Instance.GetPickUpAnswerCard ) return;

        if (IsDragging)
        {
            IsDragging = false;
        }
        GameManager.Instance.GetPickUpAnswerCard.transform.SetParent(transform);
        GameManager.Instance.GetPickUpAnswerCard.transform.localPosition = AnswerCardPosition;
        ChildSum = GameManager.Instance.GetPickUpAnswerCard.transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if ( ! ChildSum ) return;
        // Debug.Log("On pointer down on sum card and child sun is not null ? ");
        ChildSum.GetComponent<SelectArea>().ResetParent();
        ChildSum = null;
    }



    public void OnDrag(PointerEventData eventData)
    {
        if ( !GameManager.Instance.GetPickUpAnswerCard ) return;
        GameManager.Instance.GetPickUpAnswerCard.GetComponent<SelectArea>().OnDrag(eventData);
    }

}
