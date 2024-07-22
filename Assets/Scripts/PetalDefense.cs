using System;
using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using UnityEngine;

public class PetalDefense : MonoBehaviour, IFightingEntity
{
    public int passiveDefense = 3;
    public int activeDefense = 5;
    private bool _isPassive = true;
    [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;
    public void Execute(Entity target)
    {
        int defense = _isPassive ? passiveDefense : activeDefense;
        target.addArmor(defense);
    }

    public void Initialize(Entity player)
    {
        ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
        onCommandCreationEvent.Raise(command);
    }
    
    public void ActivatePetal()
    {
        _isPassive = !_isPassive;
    }
    
}