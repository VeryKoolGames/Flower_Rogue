using System;
using DefaultNamespace;
using DefaultNamespace.Events;
using KBCore.Refs;
using Player.States;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(ArmorUI), typeof(HealthUI))]
    public class Player : Entity
    { 
        [FormerlySerializedAs("playerUI")] [SerializeField, Self] private ArmorUI armorUI;
        [SerializeField, Self] private HealthUI healthUI;
        [Header("Events")]
        [SerializeField, Self] private OnTurnEndListener onTurnEndListener;
        
        // States
        private PlayerStateMachine stateMachine;
        private HighHealthState highHealthState;
        private MidHealthState midHealthState;
        private LowHealthState lowHealthState;
        private DeadState deadState;
        
        private void Start()
        {
            InitStates();
            _attributes = new EntityAttribute(entitySo.Attribute.Name, entitySo.Attribute.Health);
        }

        private void Update()
        {
            stateMachine.Update();
        }

        private void InitStates()
        {
            stateMachine = new PlayerStateMachine();
            highHealthState = new HighHealthState();
            highHealthState.Initialize(this);
            midHealthState = new MidHealthState();
            midHealthState.Initialize(this);
            lowHealthState = new LowHealthState();
            lowHealthState.Initialize(this);
            deadState = new DeadState();
            deadState.Initialize(this);
            stateMachine.ChangeState(highHealthState);
        }
    
        public override void addArmor(int amount)
        {
            _attributes.armor += amount;
            armorUI.UpdateArmor(_attributes.armor);
        }
    
        public override void loseHP(int amount)
        {
            if (_attributes.armor > 0)
            {
                amount = loseArmor(amount);
            }
            if (amount <= 0)
            {
                return;
            }
            _attributes.Health -= amount;
            healthUI.UpdateHealth(_attributes.Health, entitySo.Attribute.Health);
            UpdateState();
        }
        
        private void UpdateState()
        {
            if (_attributes.Health <= 0)
            {
                stateMachine.ChangeState(deadState);
            }
            else if (_attributes.Health <= entitySo.Attribute.Health * 0.3)
            {
                stateMachine.ChangeState(lowHealthState);
            }
            else if (_attributes.Health <= entitySo.Attribute.Health * 0.6)
            {
                stateMachine.ChangeState(midHealthState);
            }
            else
            {
                stateMachine.ChangeState(highHealthState);
            }
        }
        
        private int loseArmor(int amount)
        {
            if (_attributes.armor < amount)
            {
                amount -= _attributes.armor;
                _attributes.armor = 0;
                armorUI.UpdateArmor(_attributes.armor);
                return amount;
            }
            _attributes.armor -= amount;
            armorUI.UpdateArmor(_attributes.armor);
            return 0;
        }
    }
}