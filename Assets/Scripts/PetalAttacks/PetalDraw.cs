using Command;
using DefaultNamespace;
using DG.Tweening;
using Entities;

namespace PetalAttacks
{
    public class PetalDraw : PlayerMove, IFightingEntity, IExecuteOnClick
    {
        public ICommand commandPick { get; set; }

        public void Execute(Entity target)
        {
            RemovePetal();
        }

        public void ExecuteOnClick()
        {
            onDrawPetalEvent.Raise(gameObject);
            onPetalDeathEvent.Raise(gameObject);
            RemovePetal();
        }

        public void Initialize(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
            onCommandCreationEvent.Raise(command);
            commandPick = command;
            petalBoostsManager.Initialize(this, commandPick);

        }

        public void InitializeWithoutAdding(Entity player)
        {
            ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
            commandPick = command;
            petalBoostsManager.Initialize(this, commandPick);

        }

        public void ActivatePetal()
        {
            if (!isRedrawEnabled) return;
            onDrawPetalEvent.Raise(gameObject);
            onPetalDeathEvent.Raise(gameObject);
            RemovePetal();
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