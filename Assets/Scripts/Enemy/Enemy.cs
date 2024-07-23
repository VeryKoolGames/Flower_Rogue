using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [RequireComponent(typeof(HealthUI))]
    public class Enemy : Entity
    {
        [SerializeField] private HealthUI enemyUI;
        private void Start()
        {
            _attributes = new EntityAttribute(entitySo.Attribute.Name, entitySo.Attribute.Health);
        }
        
        public override void loseHP(int amount)
        {
            _attributes.Health -= amount;
            enemyUI.UpdateHealth(_attributes.Health, entitySo.Attribute.Health);
        }
    }
}