using Command;
using Entities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace.Events
{
    public class OnEnemySpawnListener : MonoBehaviour
    {
        public OnEnemySpawnEvent onCommandCreationEvent;
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