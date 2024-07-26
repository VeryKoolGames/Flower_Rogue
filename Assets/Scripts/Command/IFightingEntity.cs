using DefaultNamespace;
using ScriptableObjectScripts;

namespace Command
{
    public interface IFightingEntity
    {
        void Execute(Entity target);
        void Initialize(Entity player);
        void InitializeWithoutAdding(Entity player);
        void ActivatePetal();
        void ExecuteOnClick();
        void RemovePetal();
        
        public ICommand commandPick { get; set; }
    }
}