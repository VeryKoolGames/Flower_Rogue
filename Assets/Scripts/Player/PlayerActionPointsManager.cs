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
        [SerializeField] private GameObject actionPointParent;
        [SerializeField] private IntVariable startActionPoints;
        [SerializeField] private OnTurnEndListener onEnemyTurnEndListener;
        [SerializeField] private OnPetalSelectionListener onPetalSelectionListener;
        [SerializeField] private OnPetalUnSelectionListener onPetalUnSelectionListener;
        private int _actionPoints;
        private int _maxActionPoints;
        
        private void Awake()
        {
            _actionPoints = startActionPoints.Value;
            _maxActionPoints = _actionPoints;
            onEnemyTurnEndListener.Response.AddListener(ResetActionPoints);
            onPetalSelectionListener.Response.AddListener(UseActionPoint);
            onPetalUnSelectionListener.Response.AddListener(GainActionPoint);
            InstantiateActionPoints();
        }
        
        private void InstantiateActionPoints()
        {
            for (int i = 0; i < _maxActionPoints; i++)
            {
                var go = Instantiate(actionPointPrefab, actionPointParent.transform);
                actionPointsGo.Add(go);
            }
        }
        
        private void InstantiateMoreActionPoints(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var go = Instantiate(actionPointPrefab, actionPointParent.transform);
                actionPointsGo.Add(go);
            }
        }
        
        private void DestroyActionPointsFromLast(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Destroy(actionPointsGo[actionPointsGo.Count - 1]);
                actionPointsGo.RemoveAt(actionPointsGo.Count - 1);
            }
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
        
        private void UpdateActionPointsUI(int currentPoints)
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

        public void UpdateMaxActionPoints(int actionPoints)
        {
            _maxActionPoints += actionPoints;
            _actionPoints += actionPoints;
            if (actionPoints > 0)
            {
                InstantiateMoreActionPoints(actionPoints);
            }
            else
            {
                DestroyActionPointsFromLast(-actionPoints);
            }
            UpdateActionPointsUI(_maxActionPoints);
        }
    }
}