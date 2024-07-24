using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
        [SerializeField] private List<Transform> enemySpawnLocations = new List<Transform>();
        private List<IEnemyAttack> enemyAttacks = new List<IEnemyAttack>();
        [SerializeField] private Player player;
        
        private void Start()
        {
            InitializeEnemyAttacks();
            SpawnEnemies();
        }

        private void InitializeEnemyAttacks()
        {
            enemyAttacks = new List<IEnemyAttack>
            {
                new EnemyAttack(),
                new EnemyDefense(),
            };
        }
        
        private void SpawnEnemies()
        {
            for (int i = 0; i < enemySpawnLocations.Count; i++)
            {
                var enemy = Instantiate(GetRandomEnemyPrefab(), enemySpawnLocations[i].position, Quaternion.identity);
                enemy.transform.SetParent(enemySpawnLocations[i]);
                enemy.GetComponent<EnemyController>().SetEnemyAttack(enemyAttacks[Random.Range(0, enemyAttacks.Count)],
                    new Entity[] { player });
            }
        }
        
        private GameObject GetRandomEnemyPrefab()
        {
            return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        }
        
    }
}