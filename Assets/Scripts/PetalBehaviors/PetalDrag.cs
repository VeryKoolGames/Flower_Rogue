using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using DG.Tweening;
using UnityEngine;

public class PetalDrag : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDragging = false;
    private Vector3 offset;
    public Transform originalParent;
    [SerializeField] private OnPetalSpawnEvent onPetalSpawnEvent;
    [SerializeField] private OnPetalStartDraggingEvent onPetalStartDraggingEvent;
    [SerializeField] private OnPetalStopDraggingEvent onPetalStopDraggingEvent;

    void Start()
    {
        initialPosition = transform.position;
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
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse Exit");
    }
}