using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

namespace Events.PlayerMoveEvents
{
    [CreateAssetMenu(fileName = "New OnDrawPetalEvent", menuName = "ScriptableObjects/Events/PlayerMoveEvents/OnDrawPetalEvent")]
    public class OnDrawPetalEvent : ScriptableObject
    {
        private List<OnDrawPetalListener> listeners = new List<OnDrawPetalListener>();
        
        public void Raise(GameObject petal) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(petal);
            }
        }

        public void RegisterListener(OnDrawPetalListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnDrawPetalListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}