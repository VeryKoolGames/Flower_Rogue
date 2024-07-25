using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Events.PlayerMoveEvents
{
    public class OnDrawPetalListener : MonoBehaviour
    {
        public OnDrawPetalEvent onCommandCreationEvent;
        public UnityEvent<GameObject> Response;

        private void OnEnable() {
            onCommandCreationEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onCommandCreationEvent.UnregisterListener(this);
        }

        public void OnEventRaised(GameObject petal) {
            Response.Invoke(petal);
        }
    }
}