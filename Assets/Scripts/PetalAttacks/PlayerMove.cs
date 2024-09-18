using System;
using DefaultNamespace.Events;
using DG.Tweening;
using Events;
using Events.PlayerMoveEvents;
using KBCore.Refs;
using ScriptableObjectScripts;
using UI;
using UnityEngine;

namespace PetalAttacks
{
    public abstract class PlayerMove : MonoBehaviour
    {
        public int placeInHand;
        public PetalSO PetalSo;
        public OnPetalDeathEvent onPetalDeathEvent;
        public OnCommandCreationEvent onCommandCreationEvent;
        public OnTurnEndListener onRedrawTurnListener;
        public OnTurnEndListener onCombatStartListener;
        public OnDrawPetalEvent onDrawPetalEvent;
        public bool isRedrawEnabled = false;

        protected virtual void Awake()
        {
            onCombatStartListener.Response.AddListener(OnCombatStart);
            onRedrawTurnListener.Response.AddListener(OnRedrawTurnEnd);
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.25f);
        }

        private void OnCombatStart()
        {
            isRedrawEnabled = true;
            gameObject.GetComponent<PetalDrag>().enabled = false;
        }
        
        private void OnRedrawTurnEnd()
        {
            isRedrawEnabled = false;
            gameObject.GetComponent<PetalDrag>().enabled = true;
        }
        
        private void OnDisable()
        {
            onCombatStartListener.Response.RemoveListener(OnCombatStart);
            onRedrawTurnListener.Response.RemoveListener(OnRedrawTurnEnd);
        }
    }

    public abstract class PlayerAttackMove : PlayerMove
    {
        public int passiveValue;
        public int activeValue;
        public int boostCount = 0;
        public PetalBoostUI petalBoostUI;
        
        protected new virtual void Awake()
        {
            base.Awake();
            passiveValue = PetalSo.petalAttributes.passiveValue;
            activeValue = PetalSo.petalAttributes.activeValue;
        }
    }
    
    public abstract class PlayerBoostMove : PlayerMove
    {
        public int boostAmount;
    }
    
    public abstract class PlayerPassiveMove : PlayerMove
    {
        [SerializeField] protected OnTurnEndListener onTurnEndEventListener;
    }
}