using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : IEnemyAttack
    {
        public void Execute(Entity target)
        {
            target.loseHP(5);
        }

        public void Initialize(Entity player)
        {
            Debug.Log("Enemy Attack Initialized!");
        }
    }
}