using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using Events;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalAttackScaling : PlayerMove, IFightingEntity
    {
        // The scaling petals do not have use the passive value
        // They are only played when the player clicks on the petal
        [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;
        [SerializeField] private OnTurnEndListener onTurnEndEventListener;
        public ICommand commandPick { get; set; }

        private void Awake()
        {
            passiveValue = PetalSo.petalAttributes.passiveValue;
            activeValue = PetalSo.petalAttributes.activeValue;
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.25f);
            onTurnEndEventListener.Response.AddListener(OnTurnEnd);
        }
        
        private void OnTurnEnd()
        {
            activeValue += 2;
        }

        public void Execute(Entity target)
        {
            int damage = activeValue;
            target.loseHP(damage);
            RemovePetal();
        }

        public void InitializeWithoutAdding(Entity player)
        {
            // When swapping a card in the player's hand, the command is created but not added to the player's list of commands
            // it is added directly in the deckManager
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { });
            commandPick = command;
        }

        public void ActivatePetal()
        {
            commandPick.IsPreserved = false;
        }
        
        public void ExecuteOnClick()
        {
            throw new System.NotImplementedException();
        }

        public void RemovePetal()
        {
            transform.DOScale(0, 0.25f).OnComplete(() => Destroy(gameObject));
        }


        public void Initialize(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[]{ } );
            commandPick = command;
            onCommandCreationEvent.Raise(command);
        }
    }
}