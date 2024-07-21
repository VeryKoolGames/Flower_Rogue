using Command;
using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour, IFightingEntity
    {
        public void Attack(Entity target)
        {
            throw new System.NotImplementedException();
        }

        public void Defense(Entity target)
        {
            throw new System.NotImplementedException();
        }

        public void Utility(Entity target)
        {
            throw new System.NotImplementedException();
        }
    }
}