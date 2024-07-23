using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using UnityEngine;

public class PetalUtility : MonoBehaviour, IFightingEntity
{
    private bool _isPassive = true;
    [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;
    public void Execute(Entity target)
    {
        Debug.Log("PetalUtility executed!");
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

    public ICommand commandPick { get; set; }
}