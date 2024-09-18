using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using Events;
using UI;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(HealthUI))]
    public class Enemy : Entity
    {
        [SerializeField] private OnEnemyDeathEvent onEnemyDeathEvent;
        [SerializeField] private OnEnemySpawnEvent onEnemySpawnEvent;
        private void Start()
        {
            entityGameObject = gameObject;
            _attributes = new EntityAttribute(entitySo.Attribute.Name, entitySo.Attribute.Health, entitySo.Attribute.maxHealth);
            onEnemySpawnEvent.Raise(this);
            armorUI = GetComponent<ArmorUI>();
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
            if (_attributes.Health <= 0)
            {
                Die();
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
        
        public override void addArmor(int amount)
        {
            _attributes.armor += amount;
            armorUI.UpdateArmor(_attributes.armor);
        }

        private void Die()
        {
            transform.DOScale(0, 1f).OnComplete(() =>
            {
                onEnemyDeathEvent.Raise(this);
            });
        }
    }
}