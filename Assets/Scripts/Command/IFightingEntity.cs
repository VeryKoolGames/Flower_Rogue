using DefaultNamespace;
using ScriptableObjectScripts;

namespace Command
{
    public interface IFightingEntity
    {
        void Execute(Entity target);
        void Initialize(Entity player);
        void ActivatePetal();
        void RemovePetal();
        
        public ICommand commandPick { get; set; }
    }
}