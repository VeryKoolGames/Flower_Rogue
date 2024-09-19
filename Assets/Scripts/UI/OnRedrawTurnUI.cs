using Combat;
using DefaultNamespace.Events;
using UnityEngine;

namespace UI
{
    public class OnRedrawTurnUI : MonoBehaviour
    {
        [SerializeField] private GameObject redrawUIObject;
        [SerializeField] private OnTurnEndListener onCombatStartListener;
        [SerializeField] private OnTurnEndListener onPlayerTurnEvent;
        [SerializeField] private PetalRedrawManager petalRedrawManager;

        private void OnEnable()
        {
            onCombatStartListener.Response.AddListener(EnableObjects);
            onPlayerTurnEvent.Response.AddListener(DisableObjects);
        }

        private void EnableObjects()
        {
            petalRedrawManager.ResetRedrawCount();
            redrawUIObject.SetActive(true);
        }
        
        private void DisableObjects()
        {
            redrawUIObject.SetActive(false);
        }
    
        private void OnDisable()
        {
            onCombatStartListener.Response.RemoveListener(EnableObjects);
            onPlayerTurnEvent.Response.RemoveListener(DisableObjects);
        }
    }
}
