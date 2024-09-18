using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Events;
using DG.Tweening;
using UnityEngine;

namespace Combat
{
    public class CombatManager : MonoBehaviour
    {
        private List<Entity> enemyList = new List<Entity>();
        [SerializeField] private OnEnemyDeathListener onEnemyDeathListener;
        [SerializeField] private OnEnemySpawnListener onEnemySpawnListener;
        [SerializeField] private OnCombatWinEvent onCombatWinEvent;
        
        private void Awake()
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
            enemy.entityGameObject.transform.DOScale(0, .2f).OnComplete(() =>
            {
                Destroy(enemy.entityGameObject);
            });
            if (enemyList.Count == 0)
            {
                onCombatWinEvent.Raise();
            }
        }
        
        private void OnDisable()
        {
            onEnemyDeathListener.Response.RemoveListener(RemoveEnemy);
            onEnemySpawnListener.Response.RemoveListener(AddEnemy);
        }
    }
}