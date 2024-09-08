using System.Collections.Generic;
using DefaultNamespace.Events;
using ScriptableObjectScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerActionPointsManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> actionPointsGo = new List<GameObject>();
        [SerializeField] private GameObject actionPointPrefab;
        [SerializeField] private IntVariable startActionPoints;
        [SerializeField] private OnTurnEndListener onEnemyTurnEndListener;
        [SerializeField] private OnPetalSelectionListener onPetalSelectionListener;
        [SerializeField] private OnPetalUnSelectionListener onPetalUnSelectionListener;
        private int _actionPoints;
        private int _maxActionPoints;
        
        private void Start()
        {
            _actionPoints = startActionPoints.Value;
            _maxActionPoints = _actionPoints;
            onEnemyTurnEndListener.Response.AddListener(ResetActionPoints);
            onPetalSelectionListener.Response.AddListener(UseActionPoint);
            onPetalUnSelectionListener.Response.AddListener(GainActionPoint);
        }
        
        private void UseActionPoint(int cost)
        {
            if (_actionPoints < cost)
            {
                throw new System.Exception("Not enough action points");
            }
            _actionPoints -= cost;
            Debug.Log("Action points: " + _actionPoints);
            UpdateActionPointsUI(_actionPoints);
        }
    
        private void GainActionPoint(int amount)
        {
            _actionPoints += amount;
            if (_actionPoints > _maxActionPoints)
            {
                _actionPoints = _maxActionPoints;
            }
            UpdateActionPointsUI(_actionPoints);
        }
        
        private void ResetActionPoints()
        {
            _actionPoints = _maxActionPoints;
            UpdateActionPointsUI(_actionPoints);
        }
        
        public void UpdateActionPointsUI(int currentPoints)
        {
            for (int i = 0; i < actionPointsGo.Count; i++)
            {
                actionPointsGo[i].GetComponent<RawImage>().color = i < currentPoints ? Color.green : Color.red;
            }
        }
        
        private void OnDisable()
        {
            onEnemyTurnEndListener.Response.RemoveListener(ResetActionPoints);
            onPetalSelectionListener.Response.RemoveListener(UseActionPoint);
            onPetalUnSelectionListener.Response.RemoveListener(GainActionPoint);
        }
    }
}