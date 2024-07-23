using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using UnityEngine;

public class PetalUtility : MonoBehaviour, IFightingEntity
{
    private bool _isPassive = true;
    [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;
    public void Execute(Entity target)
    {
        Debug.Log("PetalUtility executed!");
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
    
    public void ActivatePetal()
    {
        _isPassive = !_isPassive;
    }
    
    public void RemovePetal()
    {
        transform.DOScale(0, 0.25f).OnComplete(() => Destroy(gameObject));
    }

    public ICommand commandPick { get; set; }
}