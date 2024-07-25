using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class OnPetalDeathListener : MonoBehaviour
    {
        public OnPetalDeathEvent onPetalDeathEvent;
        public UnityEvent<GameObject> Response;

        private void OnEnable() {
            onPetalDeathEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onPetalDeathEvent.UnregisterListener(this);
        }

        public void OnEventRaised(GameObject petal) {
            Response.Invoke(petal);
        }
    }
}