using DefaultNamespace.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class OnCombatLoseListener : MonoBehaviour
    {
        public OnCombatLoseEvent onCombatLoseEvent;
        public UnityEvent Response;

        private void OnEnable() {
            onCombatLoseEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onCombatLoseEvent.UnregisterListener(this);
        }

        public void OnEventRaised() {
            Response.Invoke();
        }
    }
}