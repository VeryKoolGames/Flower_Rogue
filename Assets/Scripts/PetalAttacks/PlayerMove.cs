using Events;
using KBCore.Refs;
using ScriptableObjectScripts;
using UI;
using UnityEngine;

namespace PetalAttacks
{
    public abstract class PlayerMove : MonoBehaviour
    {
        public int placeInHand;
        protected bool _isPassive = true;
        public PetalSO PetalSo;
        public OnPetalDeathEvent onPetalDeathEvent;
        public OnCommandCreationEvent onCommandCreationEvent;
    }

    public abstract class PlayerAttackMove : PlayerMove
    {
        public int passiveValue;
        public int activeValue;
        public int boostCount = 0;
        public PetalBoostUI petalBoostUI;
    }
    
    public abstract class PlayerBoostMove : PlayerMove
    {
        public int boostAmount;
    }
}