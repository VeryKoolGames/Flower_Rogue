using System.Collections.Generic;using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using UnityEngine;

public class PetalAttack : MonoBehaviour, IFightingEntity
{
    public int passiveDamage = 3;
    public int activeDamage = 5;
    private bool _isPassive = true;
    public ICommand commandPick;
    [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;
    public void Execute(Entity target)
    {
        int damage = _isPassive ? passiveDamage : activeDamage;
        target.loseHP(damage);
    }
    
    public void ActivatePetal()
    {
        _isPassive = !_isPassive;
    }
    
    public void Initialize(Entity player)
    {
        ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[]{} );
        commandPick = command;
        onCommandCreationEvent.Raise(command);
    }
}