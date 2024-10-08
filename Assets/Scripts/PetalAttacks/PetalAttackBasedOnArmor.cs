using Command;
using DefaultNamespace;
using DG.Tweening;
using Entities;
using Events;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalAttackBasedOnArmor : PlayerAttackMove, IFightingEntity, IPassiveActive, IKeepPlayerReference
    {
        public ICommand commandPick { get; set; }
        

        public void Execute(Entity target)
        {
            if (target == null)
            {
                Debug.Log("No target, maybe it died");
            }
            int damage = player.GetArmor();
            player.loseArmor(damage);
            damage += boostCount;
            Debug.Log("Attacking for Damage: " + damage);
            target.loseHP(damage);
            RemovePetal();
        }

        public void InitializeWithoutAdding(Entity target)
        {
            player = target;
            // When swapping a card in the player's hand, the command is created but not added to the player's list of commands
            // it is added directly in the deckManager
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] {});
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
            transform.DOScale(0, 0.25f).OnComplete(() => Destroy(gameObject));
        }


        public void Initialize(Entity target)
        {
            player = target;
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[]{} );
            commandPick = command;
            onCommandCreationEvent.Raise(command);
            petalBoostsManager.Initialize(this, commandPick);
        }

        public bool isActive { get; set; }
        public Entity player { get; set; }
    }
}