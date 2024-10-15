using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using Events;
using ScriptableObjectScripts;
using UI;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(HealthUI))]
    public class Enemy : Entity
    {
        [SerializeField] private OnEnemyDeathEvent onEnemyDeathEvent;
        [SerializeField] private OnEnemySpawnEvent onEnemySpawnEvent;
        private void Start()
        {
            entityGameObject = gameObject;
            _attributes = new EntityAttribute(entitySo.Attribute.name, entitySo.Attribute.health, entitySo.Attribute.maxHealth);
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
            _attributes.health -= amount;
            healthUI.UpdateHealth(_attributes.health, entitySo.Attribute.health);
            if (_attributes.health <= 0)
            {
                Die();
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