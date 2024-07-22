using Command;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Events
{
    public class OnPetalUnSelectionListener : MonoBehaviour
    {
        public OnPetalUnSelectionEvent onPetalUnSelectionEvent;
        public UnityEvent<int> Response;

        private void OnEnable() {
            onPetalUnSelectionEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onPetalUnSelectionEvent.UnregisterListener(this);
        }

        public void OnEventRaised(int cost) {
            Response.Invoke(cost);
        }
    }
}