using Events;
using ScriptableObjectScripts;
using UnityEngine;

namespace PetalAttacks
{
    public abstract class PlayerMove : MonoBehaviour
    {
        public int passiveValue;
        public int activeValue;
        protected bool _isPassive = true;
        public PetalSO PetalSo;
        public bool shouldPlayOnSelect = false;
        public OnPetalDeathEvent onPetalDeathEvent;
    }
}