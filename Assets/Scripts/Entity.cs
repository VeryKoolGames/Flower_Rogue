using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Entity : MonoBehaviour
    {
        public EntitySO entitySo;
        protected EntityAttribute _attributes;
        private bool isPoisoned = false;

        public virtual void loseHP(int amount){}

        public virtual void addArmor(int amount){}
        
        public int GetHealth()
        {
            return _attributes.Health;
        }
    }
}