using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class EnemyDefense : IEnemyAttack
    {
        public void Execute(Entity target)
        {
            target.addArmor(5);
        }

        public void Initialize(Entity player)
        {
            Debug.Log("Enemy Attack Initialized!");
        }
    }
}