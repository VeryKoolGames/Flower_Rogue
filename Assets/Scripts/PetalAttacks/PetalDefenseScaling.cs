using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using Events;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalDefenseScaling : PlayerAttackMove, IFightingEntity
    {
        // The scaling petals do not have use the passive value
        // They are only played when the player clicks on the petal
        [SerializeField] private OnTurnEndListener onTurnEndEventListener;
        public ICommand commandPick { get; set; }

        protected override void Awake()
        {
            base.Awake();
            onTurnEndEventListener.Response.AddListener(OnTurnEnd);
        }
        
        private void OnTurnEnd()
        {
            activeValue += 2;
        }

        public void Execute(Entity target)
        {
            commandPick.IsPreserved = false;
            int defense = activeValue;
            defense += boostCount;
            if (target is Player.Player)
                target.addArmor(defense);
            RemovePetal();
        }

        public void InitializeWithoutAdding(Entity player)
        {
            // When swapping a card in the player's hand, the command is created but not added to the player's list of commands
            // it is added directly in the deckManager
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
            commandPick = command;
            command.IsPreserved = true;
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
            commandPick.IsPreserved = false;
        }

        public void RemovePetal()
        {
            transform.DOScale(0, 0.25f).OnComplete(() => Destroy(gameObject));
        }


        public void Initialize(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[]{ player } );
            commandPick = command;
            command.IsPreserved = true;
            onCommandCreationEvent.Raise(command);
        }
    }
}