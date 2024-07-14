using UnityEngine;

namespace PetalDecorator
{
    public enum PetalType
    {
        Damage,
        Utility
    }
    [CreateAssetMenu(menuName = "Create PetalDefinition", fileName = "Petal/PetalDefinition", order = 0)]
    public class PetalDefinition : ScriptableObject
    {
        [Header("Petal Attributes")]
        public int activeValue = 5;
        public int passiveValue = 10;
        public int value;
        public PetalType type = PetalType.Damage;
        [Header("Petal Info")]
        public GameObject petalPrefab;
        public Sprite petalSprite;
        public string petalName;
        public string petalActiveDescription;
        public string petalPassiveDescription;
    }
    
    public static class PetalFactory
    {
        public static IPetal CreatePetal(PetalDefinition petalDefinition)
        {
            switch (petalDefinition.type)
            {
                case PetalType.Damage:
                    return new PetalDecorator.DamageDecorator(petalDefinition.value);
                case PetalType.Utility:
                    return new PetalDecorator.UtilityDecorator(petalDefinition.value);
                default:
                    return new PetalDecorator.DamageDecorator(petalDefinition.value);
            }
        }
    }
}