using System.Collections.Generic;
using Command;
using Entities;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "OnTargetUpdateEvent", menuName = "ScriptableObjects/Events/OnTargetUpdateEvent")]
    public class OnTargetUpdateEvent : ScriptableObject
    {
        private List<OnTargetUpdateListener> listeners = new List<OnTargetUpdateListener>();
        
        public void Raise(ICommand command, Entity[] targets) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(command, targets);
            }
        }

        public void RegisterListener(OnTargetUpdateListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnTargetUpdateListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}