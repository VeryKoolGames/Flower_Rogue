using Command;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace.Events
{
    public class OnPetalSpawnListener : MonoBehaviour
    {
        public OnPetalSpawnEvent onCommandCreationEvent;
        public UnityEvent<PetalDrag> Response;

        private void OnEnable() {
            onCommandCreationEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onCommandCreationEvent.UnregisterListener(this);
        }

        public void OnEventRaised(PetalDrag petal) {
            Response.Invoke(petal);
        }
    }
}