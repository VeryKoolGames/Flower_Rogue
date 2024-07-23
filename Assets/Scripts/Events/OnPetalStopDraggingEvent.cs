using System.Collections.Generic;
using Command;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "OnPetalStopDraggingEvent", menuName = "ScriptableObjects/Events/OnPetalStopDraggingEvent")]
    public class OnPetalStopDraggingEvent : ScriptableObject
    {
        private List<OnPetalStoptDraggingListener> listeners = new List<OnPetalStoptDraggingListener>();
        
        public void Raise() {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(OnPetalStoptDraggingListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnPetalStoptDraggingListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}