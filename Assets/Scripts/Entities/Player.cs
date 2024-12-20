using DefaultNamespace;
using DefaultNamespace.Events;
using KBCore.Refs;
using Player.States;
using ScriptableObjectScripts;
using UI;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(ArmorUI), typeof(HealthUI))]
    public class Player : Entity
    { 
        [Header("Events")]
        [SerializeField, Self] private OnTurnEndListener onTurnEndListener;
        [SerializeField] private OnCombatLoseEvent onCombatLoseEvent;
        
        // States
        private PlayerStateMachine stateMachine;
        private HighHealthState highHealthState;
        private MidHealthState midHealthState;
        private LowHealthState lowHealthState;
        private DeadState deadState;
        
        private void Start()
        {
            InitStates();
            _attributes = new EntityAttribute(entitySo.Attribute.name, entitySo.Attribute.health, entitySo.Attribute.maxHealth);
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
            _attributes.health -= amount;
            if (_attributes.health <= 0)
            {
                _attributes.health = 0;
                onCombatLoseEvent.Raise();
            }
            healthUI.UpdateHealth(_attributes.health, entitySo.Attribute.health);
            UpdateState();
        }
        
        private void UpdateState()
        {
            if (_attributes.health <= 0)
            {
                stateMachine.ChangeState(deadState);
            }
            else if (_attributes.health <= entitySo.Attribute.health * 0.3)
            {
                stateMachine.ChangeState(lowHealthState);
            }
            else if (_attributes.health <= entitySo.Attribute.health * 0.6)
            {
                stateMachine.ChangeState(midHealthState);
            }
            else
            {
                stateMachine.ChangeState(highHealthState);
            }
        }

        public override int loseArmor(int amount)
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
        
        public void UpdateMaxHealth(int amount)
        {
            _attributes.maxHealth += amount;
            healthUI.UpdateHealth(_attributes.health, _attributes.maxHealth);
        }
    }
}