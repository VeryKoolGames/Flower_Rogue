using Command;
using DefaultNamespace;
using DG.Tweening;
using Events;
using Events.PlayerMoveEvents;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalUtility : PlayerMove, IFightingEntity
    {
        [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;
        [SerializeField] private OnDrawPetalEvent onDrawPetalEvent;
        public ICommand commandPick { get; set; }

        public void Execute(Entity target)
        {
            // onDrawPetalEvent.Raise(gameObject);
            RemovePetal();
        }

        public void ExecuteOnClick()
        {
            onDrawPetalEvent.Raise(gameObject);
            onPetalDeathEvent.Raise(gameObject);
            RemovePetal();
        }
    
        private void Awake()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.25f);
            shouldPlayOnSelect = true;
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
            _isPassive = !_isPassive;
        }
    
        public void RemovePetal()
        {
            transform.DOScale(0, 0.25f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

    }
}