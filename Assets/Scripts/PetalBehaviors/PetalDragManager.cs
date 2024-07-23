using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using DG.Tweening;
using UnityEngine;

public class PetalDragManager : MonoBehaviour
{
    private List<PetalDrag> petals = new List<PetalDrag>();
    private List<ICommand> commands = new List<ICommand>();
    private PetalDrag currentPetal;
    public float swapDistance = 1f;
    [Header("Events")]
    [SerializeField] private OnPetalSpawnListener onPetalSpawnListener;
    [SerializeField] private OnPetalStartDraggingListener onPetalStartDraggingListener;
    [SerializeField] private OnPetalStoptDraggingListener onPetalEndDraggingListener;
    
    void Awake()
    {
        onPetalSpawnListener.Response.AddListener(AddPetal);
        onPetalStartDraggingListener.Response.AddListener(OnPetalStartDragging);
        onPetalEndDraggingListener.Response.AddListener(OnPetalEndDragging);
    }

    private void OnDisable()
    {
        onPetalSpawnListener.Response.RemoveListener(AddPetal);
        onPetalStartDraggingListener.Response.RemoveListener(OnPetalStartDragging);
        onPetalEndDraggingListener.Response.RemoveListener(OnPetalEndDragging);
    }
    
    private void OnPetalStartDragging(PetalDrag petal)
    {
        currentPetal = petal;
    }
    
    private void OnPetalEndDragging()
    {
        currentPetal = null;
    }

    void Update()
    {
        if (currentPetal != null)
        {
            CheckProximity(currentPetal);
        }
    }
    
    public void CheckProximity(PetalDrag draggedPetal)
    {
        foreach (var petal in petals)
        {
            if (petal != draggedPetal && Vector3.Distance(petal.transform.position, draggedPetal.transform.position) < swapDistance)
            {
                SwapPetalParent(draggedPetal, petal);
                break;
            }
        }
    }
    
    private void SwapPetalParent(PetalDrag draggedPetal, PetalDrag targetPetal)
    {
        targetPetal.transform.SetParent(draggedPetal.originalParent);
        targetPetal.transform.DOMove(draggedPetal.originalParent.position, 0.2f).SetEase(Ease.OutQuad);
        draggedPetal.originalParent = targetPetal.originalParent;
        draggedPetal.transform.SetParent(targetPetal.originalParent);
        targetPetal.originalParent = targetPetal.transform.parent;
        SwapCommands(draggedPetal.gameObject.GetComponent<IFightingEntity>().commandPick, targetPetal.gameObject.GetComponent<IFightingEntity>().commandPick);
    }
    
    private void SwapCommands(ICommand command1, ICommand command2)
    {
        var index1 = commands.IndexOf(command1);
        var index2 = commands.IndexOf(command2);
        commands[index1] = command2;
        commands[index2] = command1;
        CommandManager.Instance.commandList = commands;
    }
    
    private void AddPetal(PetalDrag petal)
    {
        petals.Add(petal);
        commands.Add(petal.gameObject.GetComponent<IFightingEntity>().commandPick);
    }
}
