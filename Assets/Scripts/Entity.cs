using System;
using KBCore.Refs;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected ArmorUI armorUI;
        [SerializeField] protected HealthUI healthUI;
        public EntitySO entitySo;
        protected EntityAttribute _attributes;
        private bool isPoisoned = false;
        public GameObject entityGameObject;

        public virtual void loseHP(int amount){}

        public virtual void addArmor(int amount){}
        
        public int GetHealth()
        {
            return _attributes.Health;
        }
    }
}