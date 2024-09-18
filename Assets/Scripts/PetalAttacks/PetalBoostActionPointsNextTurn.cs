using System;
using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using Entities;
using ScriptableObjectScripts;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalBoostActionPointsNextTurn : PlayerMove, IFightingEntity, IPassiveActive
    {
        [SerializeField] private OnNewBoostEvent onBoostEvent;
        [SerializeField] private PlayerBoostSO boost;
        public ICommand commandPick { get; set; }

        public void Execute(Entity target)
        {
            if (isActive)
            {
                onBoostEvent.Raise(boost);
            }
            RemovePetal();
        }
        
        public void Initialize(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
            onCommandCreationEvent.Raise(command);
            commandPick = command;
        }

        public void InitializeWithoutAdding(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
            commandPick = command;
        }

        public void ActivatePetal()
        {
            if (isRedrawEnabled)
            {
                onDrawPetalEvent.Raise(gameObject);
                onPetalDeathEvent.Raise(gameObject);
                RemovePetal();
                return;
            }
            isActive = !isActive;
        }
    
        public void RemovePetal()
        {
            transform.DOScale(0, 0.25f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        public bool isActive { get; set; }
    }
}