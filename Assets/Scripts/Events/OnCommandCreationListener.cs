using Command;
using Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace.Events
{
    public class OnCommandCreationListener : MonoBehaviour
    {
        [FormerlySerializedAs("onPetalTargetChoiceEvent")] public OnCommandCreationEvent onCommandCreationEvent;
        public UnityEvent<ICommand> Response;

        private void OnEnable() {
            onCommandCreationEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onCommandCreationEvent.UnregisterListener(this);
        }

        public void OnEventRaised(ICommand command) {
            Response.Invoke(command);
        }
    }
}