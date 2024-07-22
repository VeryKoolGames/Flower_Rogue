using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        private List<Entity> enemyList = new List<Entity>();
        
        public void AddEnemy(Entity enemy)
        {
            enemyList.Add(enemy);
        }
        
        public void RemoveEnemy(Entity enemy)
        {
            enemyList.Remove(enemy);
            if (enemyList.Count == 0)
            {
                Debug.Log("All enemies are dead");
            }
        }
    }
}