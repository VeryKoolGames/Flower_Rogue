using Command;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace.Events
{
    public class OnCombatWinListener : MonoBehaviour
    {
        public OnCombatWinEvent onCombatWinEvent;
        public UnityEvent Response;

        private void OnEnable() {
            onCombatWinEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onCombatWinEvent.UnregisterListener(this);
        }

        public void OnEventRaised() {
            Response.Invoke();
        }
    }
}