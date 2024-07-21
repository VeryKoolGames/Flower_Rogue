using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Entity : MonoBehaviour
    {
        public EntitySO entitySo;
        protected EntityAttribute _attributes;
        private bool isPoisoned = false;

        public void loseHP(int amount)
        {
            _attributes.Health -= amount;
            Debug.Log("Health: " + _attributes.Health);
        }
    }
}