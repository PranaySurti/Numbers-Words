using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SelectArea : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    [SerializeField] int Answer;
    [SerializeField] TMP_Text AnswerText;
    private bool isDragging = false;
    private float fixedY;
    private Vector3 DefaultPosi;
    [SerializeField] private Camera mainCamera;
    [SerializeField] Transform DefaultParent;
    private Collider objectCollider;

        void Awake()
    {
        mainCamera = Camera.main;
        DefaultPosi = transform.position;
        DefaultParent = transform.parent;
        objectCollider = GetComponent<Collider>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down on: ");
        if ( !GameManager.Instance.SetPickUp(gameObject ) ) return;

        if ( transform.parent && GetComponentInParent<DropArea>() ) 
        {
            GetComponentInParent<DropArea>().OnPointerDown(eventData); 
        }

        isDragging = true;

        if (objectCollider != null) objectCollider.enabled = false;
        fixedY = transform.position.y + 15f;
        
        Vector2 pointerScreenPos = eventData.position;
        Vector3 newWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(pointerScreenPos.x, pointerScreenPos.y, fixedY));
        transform.position = newWorldPosition;

    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if ( transform.parent && GetComponentInParent<DropArea>() ) 
        {
            GetComponentInParent<DropArea>().OnPointerUp(eventData);
        }
        else
        {
            transform.position = DefaultPosi;
        }
        
        isDragging = false;
        GameManager.Instance.DropPickup();
        if (objectCollider != null) objectCollider.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerScreenPos = Pointer.current.position.ReadValue();
        Vector3 newWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(pointerScreenPos.x, pointerScreenPos.y, fixedY));
        transform.position = newWorldPosition;
    }

    public void UpdateDrag(bool IsDrag)
    {
        isDragging = IsDrag;
    }

    public void ResetParent()
    {
        transform.SetParent(DefaultParent);
    }
}
