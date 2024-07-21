using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        initialPosition = transform.position;
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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        offset = transform.position - mousePosition;
    }

    void OnMouseUp()
    {
        isDragging = false;
        transform.DOMove(initialPosition, 0.25f);
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