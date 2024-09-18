using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using UnityEngine;

namespace PetalAttacks
{
    public class PetalPassiveDefense : PlayerPassiveMove, IFightingEntity
    {
        private Entity player;
        public void Execute(Entity target)
        {
            RemovePetal();
        }

        protected override void Awake()
        {
            base.Awake();
            onTurnEndEventListener.Response.AddListener(OnTurnEnd);
        }
        
        private void OnTurnEnd()
        {
            player.addArmor(2);
        }

        public void Initialize(Entity target)
        {
            this.player = target;
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { target });
            onCommandCreationEvent.Raise(command);
            commandPick = command;
            command.IsPreserved = true;
        }

        public void InitializeWithoutAdding(Entity target)
        {
            this.player = target;
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { target });
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
            transform.DOScale(0, 0.25f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        public ICommand commandPick { get; set; }
    }
}