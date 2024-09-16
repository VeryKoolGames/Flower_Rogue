using Command;
using ScriptableObjectScripts;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Events
{
    public class OnNewBoostListener : MonoBehaviour
    {
        public OnNewBoostEvent onNewBoostEvent;
        public UnityEvent<PlayerBoostSO> Response;

        private void OnEnable() {
            onNewBoostEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onNewBoostEvent.UnregisterListener(this);
        }

        public void OnEventRaised(PlayerBoostSO boost) {
            Response.Invoke(boost);
        }
    }
}