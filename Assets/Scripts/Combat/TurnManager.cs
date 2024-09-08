using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using Enemy;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

public enum Turn
{
    Player,
    Enemy,
    Win,
    Lose,
}

namespace DefaultNamespace.Combat
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private OnTurnEndListener onTurnEndListener;
        [SerializeField] private OnTurnEndEvent onPlayerTurnEndEvent;
        [SerializeField] private OnTurnEndEvent onEnemyTurnEndEvent;
        [SerializeField] private OnCombatWinListener onWinListener;
        [SerializeField] private OnCombatLoseListener onCombatLoseListener;
        [FormerlySerializedAs("combatWinUiManager")] [SerializeField] private CombatEndUiManager combatEndUiManager;
        private Turn currentTurn = Turn.Player;
        
        private void OnEnable()
        {
            onTurnEndListener.Response.AddListener(SwitchTurn);
            onWinListener.Response.AddListener(OnCombatWin);
            onCombatLoseListener.Response.AddListener(OnCombatLose);
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
                combatEndUiManager.OnCombatWin();
            }
            else if (currentTurn == Turn.Lose)
            {
                combatEndUiManager.OnCombatLose();
            }
        }
        
        private void OnCombatWin()
        {
            currentTurn = Turn.Win;
        }
        
        private void OnCombatLose()
        {
            currentTurn = Turn.Lose;
        }
        
        private void OnDisable()
        {
            onTurnEndListener.Response.RemoveListener(SwitchTurn);
            onWinListener.Response.RemoveListener(OnCombatWin);
            onCombatLoseListener.Response.RemoveListener(OnCombatLose);
        }
    }
}