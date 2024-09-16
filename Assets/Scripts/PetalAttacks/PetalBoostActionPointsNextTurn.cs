using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using ScriptableObjectScripts;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalBoostActionPointsNextTurn : PlayerMove, IFightingEntity
    {
        [SerializeField] private OnNewBoostEvent onBoostEvent;
        [SerializeField] private PlayerBoostSO boost;
        public ICommand commandPick { get; set; }

        public void Execute(Entity target)
        {
            if (!_isPassive)
            {
                onBoostEvent.Raise(boost);
            }
            RemovePetal();
        }

        private void Awake()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.25f);
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