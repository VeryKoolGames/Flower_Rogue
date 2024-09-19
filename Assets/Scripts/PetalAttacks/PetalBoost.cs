using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using Entities;
using Events;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalBoost : PlayerBoostMove, IFightingEntity, IPassiveActive
    {
        private int boostAmount;
        [SerializeField] private OnBoostEvent onBoostEvent;
        public ICommand commandPick { get; set; }


        public void Execute(Entity target)
        {
            RemovePetal();
        }

        public void InitializeWithoutAdding(Entity player)
        {
            // When swapping a card in the player's hand, the command is created but not added to the player's list of commands
            // it is added directly in the deckManager
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
            commandPick = command;
            boostAmount = petalBoostSo.boostAmount;
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
            boostAmount = isActive ? boostAmount * 2 : boostAmount / 2;
            onBoostEvent.Raise();
        }

        public void RemovePetal()
        {
            transform.DOScale(0, 0.25f).OnComplete(() => Destroy(gameObject));
        }


        public void Initialize(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[]{ player } );
            commandPick = command;
            onCommandCreationEvent.Raise(command);
            boostAmount = petalBoostSo.boostAmount;
            petalBoostsManager.Initialize(this, commandPick);

        }
        
        public override void ApplyBoostEffect(PlayerMove target)
        {
            if (target is PlayerAttackMove petalAttack)
            {
                target.petalBoostsManager.ApplyBoost(petalBoostSo);
                petalAttack.petalBoostUI.ApplyBoostingEffect();
            }
        }

        public bool isActive { get; set; }
    }
}