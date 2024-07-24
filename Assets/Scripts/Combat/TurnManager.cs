using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using Enemy;
using UnityEngine;

public enum Turn
{
    Player,
    Enemy
}

namespace DefaultNamespace.Combat
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private OnTurnEndListener onTurnEndListener;
        private Turn currentTurn = Turn.Player;
        
        private void OnEnable()
        {
            onTurnEndListener.Response.AddListener(StartEnemyTurn);
        }
        
        private void StartEnemyTurn()
        {
            EnemyCommandManager.Instance.ExecuteCommand();
        }
        
        private void OnDisable()
        {
            onTurnEndListener.Response.RemoveListener(StartEnemyTurn);
        }
        
    }
}