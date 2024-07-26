using Command;
using DefaultNamespace.Events;
using Events;
using PetalAttacks;
using UnityEngine;

namespace PetalBehaviors
{
    public class ActivatePetal : MonoBehaviour
    {
        private bool _isMouseOver = false;
        private bool _startTimer = false;
        private float _timeBetweenClicks;
        private Color _baseColor;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private OnPetalSelectionEvent onPetalSelectionEvent;
        [SerializeField] private OnPetalUnSelectionEvent onPetalUnSelectionEvent;
        private IFightingEntity _petal;
        private int _cost;
        private PlayerMove _playerMove;
    
        void Start()
        {
            _petal = GetComponent<IFightingEntity>();
            _baseColor = GetComponent<SpriteRenderer>().color;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playerMove = GetComponent<PlayerMove>();
            _cost = _playerMove.PetalSo.petalAttributes.cost;
        }

        void Update()
        {
            if (_isMouseOver && Input.GetMouseButtonDown(0))
            {
                _startTimer = true;
            }
            if (_isMouseOver && Input.GetMouseButtonUp(0) && _startTimer)
            {
                if (_timeBetweenClicks < 0.2f)
                {
                    OnClick();
                }
                _startTimer = false;
                _timeBetweenClicks = 0;
            }
            if (_startTimer)
            {
                _timeBetweenClicks += Time.deltaTime;
            }
        }

        void OnMouseEnter()
        {
            _isMouseOver = true;
        }

        void OnMouseExit()
        {
            _isMouseOver = false;
        }

        private void OnClick()
        {
            if (_playerMove.shouldPlayOnSelect)
            {
                if (onPetalSelectionEvent.Raise(_cost))
                {
                    _petal.ExecuteOnClick();
                }
            }
            else if (_spriteRenderer.color == _baseColor)
            {
                if (onPetalSelectionEvent.Raise(_cost))
                {
                    SetColorToHalf();
                }
            }
            else
            {
                onPetalUnSelectionEvent.Raise(_cost);
                SetColorToBase();
            }
            _petal.ActivatePetal();
        }

        private void SetColorToBase()
        {
            _spriteRenderer.color = _baseColor;
        }

        private void SetColorToHalf()
        {
            Color color = _baseColor;
            color.r = 0.5f;
            _spriteRenderer.color = color;
        }
    }
}
