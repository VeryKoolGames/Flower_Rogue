using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using Events;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalBoost : PlayerMove, IFightingEntity
    {
        [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;
        [SerializeField] private OnBoostEvent onBoostEvent;
        public ICommand commandPick { get; set; }
        public bool boostLeft = true;
        public bool boostRight = true;
        public int boostAmount = 10;

        private void Awake()
        {
            passiveValue = PetalSo.petalAttributes.passiveValue;
            activeValue = PetalSo.petalAttributes.activeValue;
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.25f);
        }

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
        }

        public void ActivatePetal()
        {
            _isPassive = !_isPassive;
            boostAmount = _isPassive ? boostAmount * 2 : boostAmount / 2;
            onBoostEvent.Raise();
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
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[]{ player } );
            commandPick = command;
            onCommandCreationEvent.Raise(command);
        }
    }
}