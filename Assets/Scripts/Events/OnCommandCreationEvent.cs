using System.Collections.Generic;
using Command;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "New OnPetalTargetChoiceEvent", menuName = "ScriptableObjects/Events/OnPetalTargetChoiceEvent")]
    public class OnCommandCreationEvent : ScriptableObject
    {
        private List<OnCommandCreationListener> listeners = new List<OnCommandCreationListener>();
        
        public void Raise(ICommand command) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(command);
            }
        }

        public void RegisterListener(OnCommandCreationListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnCommandCreationListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}