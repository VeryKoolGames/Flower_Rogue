using Command;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Events
{
    public class OnPetalSelectionListener : MonoBehaviour
    {
        public OnPetalSelectionEvent onPetalSelectionEvent;
        public UnityEvent<int> Response;

        private void OnEnable() {
            onPetalSelectionEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onPetalSelectionEvent.UnregisterListener(this);
        }

        public bool OnEventRaised(int cost) {
            try {
                Response.Invoke(cost);
                return true; // Indicate success
            } catch {
                return false; // Indicate failure
            }
        }
    }
}