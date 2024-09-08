using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using Enemy;
using UnityEngine;

public enum Turn
{
    Player,
    Enemy,
    Win,
}

namespace DefaultNamespace.Combat
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private OnTurnEndListener onTurnEndListener;
        [SerializeField] private OnTurnEndEvent onPlayerTurnEndEvent;
        [SerializeField] private OnTurnEndEvent onEnemyTurnEndEvent;
        [SerializeField] private OnCombatWinListener onWinListener;
        [SerializeField] private CombatWinUiManager combatWinUiManager;
        private Turn currentTurn = Turn.Player;
        
        private void OnEnable()
        {
            onTurnEndListener.Response.AddListener(SwitchTurn);
            onWinListener.Response.AddListener(OnCombatWin);
        }
        
        private void SwitchTurn()
        {
            if (currentTurn == Turn.Player)
            {
                currentTurn = Turn.Enemy;
                EnemyCommandManager.Instance.ExecuteCommand();
            }
            else if (currentTurn == Turn.Enemy)
            {
                currentTurn = Turn.Player;
                onEnemyTurnEndEvent.Raise();
            }
            else if (currentTurn == Turn.Win)
            {
                combatWinUiManager.OnCombatWin();
            }
        }
        
        private void OnCombatWin()
        {
            currentTurn = Turn.Win;
        }
        
        private void OnDisable()
        {
            onTurnEndListener.Response.RemoveListener(SwitchTurn);
            onWinListener.Response.RemoveListener(OnCombatWin);
        }
        
    }
}