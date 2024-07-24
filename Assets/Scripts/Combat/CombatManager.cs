using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

namespace DefaultNamespace.Combat
{
    public class CombatManager : MonoBehaviour
    {
        private List<Entity> enemyList = new List<Entity>();
        [SerializeField] private OnEnemyDeathListener onEnemyDeathListener;
        [SerializeField] private OnEnemySpawnListener onEnemySpawnListener;
        
        private void OnEnable()
        {
            onEnemyDeathListener.Response.AddListener(RemoveEnemy);
            onEnemySpawnListener.Response.AddListener(AddEnemy);
        }
        
        private void AddEnemy(Entity enemy)
        {
            enemyList.Add(enemy);
        }
        
        private void RemoveEnemy(Entity enemy)
        {
            enemyList.Remove(enemy);
            if (enemyList.Count == 0)
            {
                Debug.Log("All enemies are dead!");
            }
        }
        
        private void OnDisable()
        {
            onEnemyDeathListener.Response.RemoveListener(RemoveEnemy);
            onEnemySpawnListener.Response.RemoveListener(AddEnemy);
        }
    }
}