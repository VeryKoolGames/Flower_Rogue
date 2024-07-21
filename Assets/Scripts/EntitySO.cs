using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class EntityAttribute
    {
        public string Name;
        public int Health;

        public EntityAttribute(string name, int health)
        {
            Name = name;
            Health = health;
        }
    }
    
    [CreateAssetMenu(fileName = "New Entity", menuName = "ScriptableObjects/Entity")]
    public class EntitySO : ScriptableObject
    {
        public EntityAttribute Attribute;
    }
}