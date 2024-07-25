using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using Events;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalDefense : PlayerMove, IFightingEntity
    {
        [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;
    
        public void Execute(Entity target)
        {
            int defense = _isPassive ? passiveValue : activeValue;
            if (target is Player.Player)
                target.addArmor(defense);
            RemovePetal();
        }
    
        private void Awake()
        {
            passiveValue = 3;
            activeValue = 5;
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.25f);
        }

        public void Initialize(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
            onCommandCreationEvent.Raise(command);
            commandPick = command;
        }
    
        public void ActivatePetal()
        {
            _isPassive = !_isPassive;
        }
    
        public void RemovePetal()
        {
            onPetalDeathEvent.Raise(gameObject);
            transform.DOScale(0, 0.25f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        public ICommand commandPick { get; set; }
    }
}