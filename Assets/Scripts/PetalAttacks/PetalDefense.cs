using Command;
using DefaultNamespace;
using DG.Tweening;
using Entities;
using Events;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalDefense : PlayerAttackMove, IFightingEntity, IPassiveActive
    {
        public void Execute(Entity target)
        {
            int defense = isActive ? activeValue : passiveValue;
            defense += boostCount;
            if (target is Entities.Player)
                target.addArmor(defense);
            RemovePetal();
        }

        public void Initialize(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
            onCommandCreationEvent.Raise(command);
            commandPick = command;
            petalBoostsManager.Initialize(this, commandPick);

        }

        public void InitializeWithoutAdding(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
            commandPick = command;
            petalBoostsManager.Initialize(this, commandPick);

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

        public ICommand commandPick { get; set; }
        public bool isActive { get; set; }
    }
}