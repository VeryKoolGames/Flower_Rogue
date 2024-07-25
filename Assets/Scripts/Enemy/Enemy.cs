using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using UI;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(HealthUI))]
    public class Enemy : Entity
    {
        [SerializeField] private HealthUI enemyUI;
        [SerializeField] private OnEnemyDeathEvent onEnemyDeathEvent;
        [SerializeField] private OnEnemySpawnEvent onEnemySpawnEvent;
        private ArmorUI armorUI;
        private void Start()
        {
            _attributes = new EntityAttribute(entitySo.Attribute.Name, entitySo.Attribute.Health);
            onEnemySpawnEvent.Raise(this);
            armorUI = GetComponent<ArmorUI>();
        }
        
        public override void loseHP(int amount)
        {
            _attributes.Health -= amount;
            enemyUI.UpdateHealth(_attributes.Health, entitySo.Attribute.Health);
            if (_attributes.Health <= 0)
            {
                Die();
            }
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
                Destroy(gameObject);
            });
        }
    }
}