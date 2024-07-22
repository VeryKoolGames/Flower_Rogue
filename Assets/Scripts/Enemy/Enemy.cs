using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Enemy : Entity
    {
        private void Start()
        {
            _attributes = new EntityAttribute(entitySo.Attribute.Name, entitySo.Attribute.Health);
            
        }
    }
}