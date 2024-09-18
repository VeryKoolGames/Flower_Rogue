using DefaultNamespace;
using UI;
using UnityEngine;

namespace Entities
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
        
        public int GetArmor()
        {
            return _attributes.armor;
        }

        public virtual void addArmor(int amount){}
        public virtual int loseArmor(int amount)
        {
            return 0;
        }
        
        public int GetHealth()
        {
            return _attributes.Health;
        }
    }
}