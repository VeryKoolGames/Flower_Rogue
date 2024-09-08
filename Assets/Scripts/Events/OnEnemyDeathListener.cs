using Command;
using Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace.Events
{
    public class OnEnemyDeathListener : MonoBehaviour
    {
        public OnEnemyDeathEvent onCommandCreationEvent;
        public UnityEvent<Entity> Response;

        private void OnEnable() {
            onCommandCreationEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onCommandCreationEvent.UnregisterListener(this);
        }

        public void OnEventRaised(Entity enemy) {
            Response.Invoke(enemy);
        }
    }
}