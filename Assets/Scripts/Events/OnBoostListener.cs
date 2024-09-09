using DefaultNamespace.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class OnBoostListener : MonoBehaviour
    {
        public OnBoostEvent onBoostEvent;
        public UnityEvent Response;

        private void OnEnable() {
            onBoostEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onBoostEvent.UnregisterListener(this);
        }

        public void OnEventRaised() {
            Response.Invoke();
        }
    }
}