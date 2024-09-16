using System.Collections.Generic;
using Command;
using ScriptableObjectScripts;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "New OnNewBoostEvent", menuName = "ScriptableObjects/Events/OnNewBoostEvent")]
    public class OnNewBoostEvent : ScriptableObject
    {
        private List<OnNewBoostListener> listeners = new List<OnNewBoostListener>();
        
        public void Raise(PlayerBoostSO boost) {
            for (int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(boost);
            }
        }

        public void RegisterListener(OnNewBoostListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnNewBoostListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}