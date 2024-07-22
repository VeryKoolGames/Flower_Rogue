using Command;
using DefaultNamespace;
using UnityEngine;

public class PetalUtility : MonoBehaviour, IFightingEntity
{
    public void Execute(Entity target)
    {
        Debug.Log("PetalUtility executed!");
    }

    public void Initialize(Entity player)
    {
        ICommand command = CommandFactory.CreateCommand(GetComponent<IFightingEntity>(), new Entity[] { player });
    }
}