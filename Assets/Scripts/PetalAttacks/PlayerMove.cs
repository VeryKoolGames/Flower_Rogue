using Events;
using ScriptableObjectScripts;
using UnityEngine;

namespace PetalAttacks
{
    public abstract class PlayerMove : MonoBehaviour
    {
        public int placeInHand;
        public int passiveValue;
        public int activeValue;
        protected bool _isPassive = true;
        public PetalSO PetalSo;
        public bool shouldPlayOnSelect = false;
        public OnPetalDeathEvent onPetalDeathEvent;
        public void Decorate(int amount)
        {
            activeValue += amount;
            passiveValue += amount;
        }
        public void RemoveDecorations()
        {
            activeValue = PetalSo.petalAttributes.activeValue;
            passiveValue = PetalSo.petalAttributes.passiveValue;
        }
        public void RemoveDecorator(int amount)
        {
            activeValue -= amount;
            passiveValue -= amount;
        }
    }
}