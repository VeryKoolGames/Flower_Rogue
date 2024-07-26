using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using Events;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalAttack : PlayerMove, IFightingEntity
    {
        [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;
        public ICommand commandPick { get; set; }

        private void Awake()
        {
            passiveValue = 3;
            activeValue = 5;
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.25f);
        }

        public void Execute(Entity target)
        {
            int damage = _isPassive ? passiveValue : activeValue;
            target.loseHP(damage);
            RemovePetal();
        }

        public void InitializeWithoutAdding(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
            commandPick = command;
        }

        public void ActivatePetal()
        {
            _isPassive = !_isPassive;
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
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[]{} );
            commandPick = command;
            onCommandCreationEvent.Raise(command);
        }
    }
}