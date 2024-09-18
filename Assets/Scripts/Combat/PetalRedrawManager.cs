using System;
using DefaultNamespace.Combat;
using ScriptableObjectScripts;
using TMPro;
using UnityEngine;

namespace Combat
{
    public class PetalRedrawManager : MonoBehaviour
    {
        private int _redrawCount = 0;
        private int _maxRedraws = 0;
        [SerializeField] private IntVariable maxRedraws;
        [SerializeField] private TurnManager turnManager;
        [SerializeField] private TextMeshProUGUI redrawCountText;


        private void Awake()
        {
            _maxRedraws = maxRedraws.Value;
        }

        private void Start()
        {
            UpdateRedrawCountText();
        }

        public void OnRedraw()
        {
            _redrawCount++;
            if (_redrawCount >= _maxRedraws)
            {
                OnRedrawMaxReached();
                return;
            }
            UpdateRedrawCountText();
        }
    
        private void OnRedrawMaxReached()
        {
            _redrawCount = 0;
            turnManager.SwitchTurn();
        }
        
        private void UpdateRedrawCountText()
        {
            redrawCountText.text = "Redraws left : " + (_maxRedraws - _redrawCount);
        }
        
    }
}
