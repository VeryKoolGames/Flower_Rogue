using System;
using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using Enemy;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public enum Turn
{
    Player,
    Enemy,
    Win,
    Lose,
    Redraw,
}

namespace DefaultNamespace.Combat
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private OnTurnEndListener onTurnEndListener;
        [SerializeField] private OnTurnEndEvent onEnemyTurnEndEvent;
        [SerializeField] private OnTurnEndEvent onRedrawTurnEvent;
        [SerializeField] private OnTurnEndEvent onPlayerTurnEndEvent;
        [SerializeField] private OnCombatWinListener onWinListener;
        [SerializeField] private OnCombatLoseListener onCombatLoseListener;
        [FormerlySerializedAs("combatWinUiManager")] [SerializeField] private CombatEndUiManager combatEndUiManager;
        [SerializeField] private TextMeshProUGUI currentPhaseText;
        private Turn currentTurn = Turn.Redraw;
        
        private void Start()
        {
            onTurnEndListener.Response.AddListener(SwitchTurn);
            onWinListener.Response.AddListener(OnCombatWin);
            onCombatLoseListener.Response.AddListener(OnCombatLose);
            UpdateCurrentPhaseText();
        }
        
        public void SwitchTurn()
        {
            switch (currentTurn)
            {
                case Turn.Redraw:
                    currentTurn = Turn.Player;
                    onRedrawTurnEvent.Raise();
                    break;
                case Turn.Player:
                    currentTurn = Turn.Enemy;
                    onPlayerTurnEndEvent.Raise();
                    break;
                case Turn.Enemy:
                    currentTurn = Turn.Redraw;
                    onEnemyTurnEndEvent.Raise();
                    break;
                case Turn.Win:
                    combatEndUiManager.OnCombatWin();
                    break;
                case Turn.Lose:
                    combatEndUiManager.OnCombatLose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            UpdateCurrentPhaseText();
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

        private void UpdateCurrentPhaseText()
        {
            currentPhaseText.text = "Current Phase: --" + currentTurn + "--";
        }
    }
}