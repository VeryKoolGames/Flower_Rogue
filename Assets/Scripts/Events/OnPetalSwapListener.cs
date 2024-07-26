using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class OnPetalSwapListener : MonoBehaviour
    {
        public OnPetalSwapEvent onPetalSwapEvent;
        public UnityEvent<PetalDrag, int> Response;

        private void OnEnable() {
            onPetalSwapEvent.RegisterListener(this);
        }

        private void OnDisable() {
            onPetalSwapEvent.UnregisterListener(this);
        }

        public void OnEventRaised(PetalDrag petal, int index) {
            Response.Invoke(petal, index);
        }
    }
}