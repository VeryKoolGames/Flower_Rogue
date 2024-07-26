using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New OnPetalSwapEvent", menuName = "ScriptableObjects/Events/OnPetalSwapEvent")]
    public class OnPetalSwapEvent : ScriptableObject
    {
        private List<OnPetalSwapListener> listeners = new List<OnPetalSwapListener>();
        
        public void Raise(PetalDrag petal, int index) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(petal, index);
            }
        }

        public void RegisterListener(OnPetalSwapListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnPetalSwapListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}