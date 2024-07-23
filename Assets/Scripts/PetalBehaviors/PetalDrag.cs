using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using DG.Tweening;
using UnityEngine;

public class PetalDrag : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public Transform originalParent;
    [SerializeField] private OnPetalSpawnEvent onPetalSpawnEvent;
    [SerializeField] private OnPetalStartDraggingEvent onPetalStartDraggingEvent;
    [SerializeField] private OnPetalStopDraggingEvent onPetalStopDraggingEvent;
    public Vector3 targetRotation;

    void Start()
    {
        onPetalSpawnEvent.Raise(this);
        originalParent = transform.parent;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition + offset;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        onPetalStartDraggingEvent.Raise(this);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        offset = transform.position - mousePosition;
    }
    
    void OnMouseUp()
    {
        isDragging = false;
        onPetalStopDraggingEvent.Raise();
        transform.DOMove(transform.parent.position, 0.25f);
        if (targetRotation != Vector3.zero)
            transform.DORotate(targetRotation, 0.25f);
        targetRotation = Vector3.zero;
    }

    // void OnMouseEnter()
    // {
    //     Debug.Log("Mouse Enter");
    // }
    //
    // void OnMouseExit()
    // {
    //     Debug.Log("Mouse Exit");
    // }
}