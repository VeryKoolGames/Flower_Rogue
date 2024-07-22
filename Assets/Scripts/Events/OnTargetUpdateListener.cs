using Command;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace.Events
{
    public class OnTargetUpdateListener : MonoBehaviour
    {
        public OnTargetUpdateEvent onTargetUpdateEvent;
        public UnityEvent<ICommand, Entity[]> Response;

        private void OnEnable() {
            onTargetUpdateEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onTargetUpdateEvent.UnregisterListener(this);
        }

        public void OnEventRaised(ICommand command, Entity[] targets) {
            Response.Invoke(command, targets);
        }
    }
}