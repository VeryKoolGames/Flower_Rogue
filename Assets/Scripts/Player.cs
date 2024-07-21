using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Entity
{ 
    private void Start()
    {
        _attributes = new EntityAttribute(entitySo.Attribute.Name, entitySo.Attribute.Health);
    }
}