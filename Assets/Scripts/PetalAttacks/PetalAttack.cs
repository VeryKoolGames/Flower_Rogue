using System;
using System.Collections.Generic;using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using UnityEngine;

public class PetalAttack : MonoBehaviour, IFightingEntity
{
    public int passiveDamage = 3;
    public int activeDamage = 5;
    private bool _isPassive = true;
    [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.25f);
    }

    public void Execute(Entity target)
    {
        int damage = _isPassive ? passiveDamage : activeDamage;
        target.loseHP(damage);
        RemovePetal();
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

    public void Initialize(Entity player)
    {
        ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[]{} );
        commandPick = command;
        onCommandCreationEvent.Raise(command);
    }
}