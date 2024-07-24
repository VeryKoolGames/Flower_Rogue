using DefaultNamespace;
using DefaultNamespace.Events;
using Player.States;
using UnityEngine;

namespace Player
{
    public class Player : Entity
    { 
        private int actionPoints = 4;
        private int maxActionPoints;
        [SerializeField] private PlayerUI playerUI;
        [SerializeField] private HealthUI healthUI;
        [Header("Events")]
        [SerializeField] private OnPetalSelectionListener onPetalSelectionListener;
        [SerializeField] private OnPetalUnSelectionListener onPetalUnSelectionListener;
        
        // States
        private PlayerStateMachine stateMachine;
        private HighHealthState highHealthState;
        private MidHealthState midHealthState;
        private LowHealthState lowHealthState;
        private DeadState deadState;
        
        private void Start()
        {
            InitStates();
            maxActionPoints = actionPoints;
            onPetalSelectionListener.Response.AddListener(UseActionPoint);
            onPetalUnSelectionListener.Response.AddListener(GainActionPoint);
            _attributes = new EntityAttribute(entitySo.Attribute.Name, entitySo.Attribute.Health);
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
    
    
        private void UseActionPoint(int cost)
        {
            if (actionPoints < cost)
            {
                Debug.Log("Not enough action points");
                throw new System.Exception("Not enough action points");
            }
            actionPoints -= cost;
            Debug.Log("Action points: " + actionPoints);
            playerUI.UpdateActionPoints(actionPoints);
        }
    
        private void GainActionPoint(int amount)
        {
            actionPoints += amount;
            if (actionPoints > maxActionPoints)
            {
                actionPoints = maxActionPoints;
            }
            playerUI.UpdateActionPoints(actionPoints);
        }
    
        public override void addArmor(int amount)
        {
            _attributes.armor += amount;
            playerUI.UpdateArmor(_attributes.armor);
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
                playerUI.UpdateArmor(_attributes.armor);
                return amount;
            }
            _attributes.armor -= amount;
            playerUI.UpdateArmor(_attributes.armor);
            return 0;
        }
    
        private void OnDisable()
        {
            onPetalSelectionListener.Response.RemoveListener(UseActionPoint);
            onPetalUnSelectionListener.Response.RemoveListener(GainActionPoint);
        }
    }
}