using System.Collections.Generic;
using Command;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "OnPetalStartDraggingEvent", menuName = "ScriptableObjects/Events/OnPetalStartDraggingEvent")]
    public class OnPetalStartDraggingEvent : ScriptableObject
    {
        private List<OnPetalStartDraggingListener> listeners = new List<OnPetalStartDraggingListener>();
        
        public void Raise(PetalDrag petal) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(petal);
            }
        }

        public void RegisterListener(OnPetalStartDraggingListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnPetalStartDraggingListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}