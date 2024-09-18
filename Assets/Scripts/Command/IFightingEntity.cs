using DefaultNamespace;
using DefaultNamespace.Events;
using PetalAttacks;
using ScriptableObjectScripts;
using UnityEngine;

namespace Command
{
    public interface IFightingEntity
    {
        void Execute(Entity target);
        void Initialize(Entity player);
        void InitializeWithoutAdding(Entity player);
        void ActivatePetal();
        void RemovePetal();
        
        public ICommand commandPick { get; set; }
    }
    
    public interface IExecuteOnClick
    {
        void ExecuteOnClick();
    }

    public interface IPassiveActive
    {
        public bool isActive { get; set; }
    }
}