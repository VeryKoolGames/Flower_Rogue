using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class EntityAttribute
    {
        public string Name;
        public int Health;
        public int armor;
        public int maxHealth;

        public EntityAttribute(string name, int health, int maxHealth)
        {
            Name = name;
            Health = health;
            this.maxHealth = maxHealth;
        }
    }
    
    [CreateAssetMenu(fileName = "New Entity", menuName = "ScriptableObjects/Entity")]
    public class EntitySO : ScriptableObject
    {
        public EntityAttribute Attribute;
    }
}