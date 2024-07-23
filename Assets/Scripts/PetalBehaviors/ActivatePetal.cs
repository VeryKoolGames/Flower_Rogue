using Command;
using DefaultNamespace.Events;
using UnityEngine;

public class ActivatePetal : MonoBehaviour
{
    private bool isMouseOver = false;
    private bool startTimer = false;
    private float timeBetweenClicks;
    private Color baseColor;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private OnPetalSelectionEvent onPetalSelectionEvent;
    [SerializeField] private OnPetalUnSelectionEvent onPetalUnSelectionEvent;
    private IFightingEntity _petal;
    
    void Start()
    {
        _petal = GetComponent<IFightingEntity>();
        baseColor = GetComponent<SpriteRenderer>().color;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButtonDown(0))
        {
            startTimer = true;
        }
        if (isMouseOver && Input.GetMouseButtonUp(0) && startTimer)
        {
            if (timeBetweenClicks < 0.2f)
            {
                OnClick();
            }
            startTimer = false;
            timeBetweenClicks = 0;
        }
        if (startTimer)
        {
            timeBetweenClicks += Time.deltaTime;
        }
    }

    void OnMouseEnter()
    {
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        isMouseOver = false;
    }

    void OnClick()
    {
        UpdatePetalState();
    }
    
    private void UpdatePetalState()
    {
        if (_spriteRenderer.color == baseColor)
        {
            if (onPetalSelectionEvent.Raise(3))
            {
                SetColorToHalf();
            }
        }
        else
        {
            onPetalUnSelectionEvent.Raise(3);
            SetColorToBase();
        }
        _petal.ActivatePetal();
    }

    private void SetColorToBase()
    {
        _spriteRenderer.color = baseColor;
    }

    private void SetColorToHalf()
    {
        Color color = baseColor;
        color.r = 0.5f;
        _spriteRenderer.color = color;
    }
}
