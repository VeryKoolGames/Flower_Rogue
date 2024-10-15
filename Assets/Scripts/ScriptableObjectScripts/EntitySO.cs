using System;
using UnityEngine;

namespace ScriptableObjectScripts
{
    [Serializable]
    public class EntityAttribute
    {
        public string name;
        public int health;
        public int armor;
        public int maxHealth;

        public EntityAttribute(string name, int health, int maxHealth)
        {
            this.name = name;
            this.health = health;
            this.maxHealth = maxHealth;
        }
    }
    
    [CreateAssetMenu(fileName = "New Entity", menuName = "ScriptableObjects/Entity")]
    public class EntitySO : ScriptableObject
    {
        public EntityAttribute Attribute;
    }
}