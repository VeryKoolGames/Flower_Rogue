using Command;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace.Events
{
    public class OnPetalStoptDraggingListener : MonoBehaviour
    {
        public OnPetalStopDraggingEvent onCommandCreationEvent;
        public UnityEvent Response;

        private void OnEnable() {
            onCommandCreationEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onCommandCreationEvent.UnregisterListener(this);
        }

        public void OnEventRaised() {
            Response.Invoke();
        }
    }
}