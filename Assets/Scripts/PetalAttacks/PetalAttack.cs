using Command;
using DefaultNamespace;
using DG.Tweening;
using Entities;
using Events;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalAttack : PlayerAttackMove, IFightingEntity, IPassiveActive
    {
        public ICommand commandPick { get; set; }
        

        public void Execute(Entity target)
        {
            if (target == null)
            {
                Debug.Log("No target, maybe it died");
            }
            int damage = isActive ? activeValue : passiveValue;
            damage += boostCount;
            Debug.Log("Attacking for Damage: " + damage);
            target.loseHP(damage);
            RemovePetal();
        }

        public void InitializeWithoutAdding(Entity player)
        {
            // When swapping a card in the player's hand, the command is created but not added to the player's list of commands
            // it is added directly in the deckManager
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] {});
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
            transform.DOScale(0, 0.25f).OnComplete(() => Destroy(gameObject));
        }


        public void Initialize(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[]{} );
            commandPick = command;
            onCommandCreationEvent.Raise(command);
        }

        public bool isActive { get; set; }
    }
}