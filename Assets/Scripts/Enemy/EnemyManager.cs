using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Events;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
        [SerializeField] private List<Transform> enemySpawnLocations = new List<Transform>();
        
        private void Start()
        {
            SpawnEnemies();
        }
        
        private void SpawnEnemies()
        {
            for (int i = 0; i < enemySpawnLocations.Count; i++)
            {
                var enemy = Instantiate(GetRandomEnemyPrefab(), enemySpawnLocations[i].position, Quaternion.identity);
                enemy.transform.SetParent(enemySpawnLocations[i]);
            }
        }
        
        private GameObject GetRandomEnemyPrefab()
        {
            return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        }
    }
}