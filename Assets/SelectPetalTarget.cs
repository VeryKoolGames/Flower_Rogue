using Command;
using DefaultNamespace.Events;
using PetalDecorator;
using UnityEngine;
using UnityEngine.Serialization;
using ICommand = System.Windows.Input.ICommand;

public class SelectPetalTarget : MonoBehaviour
{
    [FormerlySerializedAs("onPetalTargetChoiceEvent")] [SerializeField] private OnCommandCreationEvent onCommandCreationEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            AttackCommand command = PlayerCommand.Create<AttackCommand>(GetComponent<PlayerAttacks>(),
                new[] { other.GetComponent<DefaultNamespace.Enemy>() });
            onCommandCreationEvent.Raise(command);
        }
    }
}
