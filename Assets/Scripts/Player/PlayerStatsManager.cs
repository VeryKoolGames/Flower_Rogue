using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using ScriptableObjectScripts;
using UnityEngine;

namespace Player
{
    public class StatBoostData
    {
        public int boostAmount;
        public int duration;

        public StatBoostData(int amount, int duration)
        {
            this.boostAmount = amount;
            this.duration = duration;
        }
    }
    public class PlayerStatsManager : MonoBehaviour
    {
        private int healthBoost;
        private int actionPoints;
        private Dictionary<StatType, List<StatBoostData>> statBoosts = new Dictionary<StatType, List<StatBoostData>>();
        [SerializeField] private OnTurnEndListener onTurnEndListener;
        [SerializeField] private Player playerHealth;
        [SerializeField] private PlayerActionPointsManager playerActionPointsManager;
        [SerializeField] private OnNewBoostListener onNewBoostListener;
        
        private void Awake()
        {
            onTurnEndListener.Response.AddListener(RemoveBoostDuration);
            onNewBoostListener.Response.AddListener(ApplyBoost);
        }

        private void OnDestroy()
        {
            onTurnEndListener.Response.RemoveListener(RemoveBoostDuration);
            onNewBoostListener.Response.RemoveListener(ApplyBoost);
        }

        private void ApplyBoost(PlayerBoostSO boost)
        {
            if (!statBoosts.ContainsKey(boost.statType))
            {
                statBoosts[boost.statType] = new List<StatBoostData>();
            }

            var newBoost = new StatBoostData(boost.boostAmount, boost.duration);
            statBoosts[boost.statType].Add(newBoost);
            // UpdateStats();
        }

        private void UpdateStats()
        {
            healthBoost = 0;
            actionPoints = 0;
            if (statBoosts.TryGetValue(StatType.Health, out var statBoost))
            {
                foreach (var boost in statBoost)
                {
                    healthBoost += boost.boostAmount;
                }
                playerHealth.UpdateMaxHealth(healthBoost);
            }

            if (statBoosts.TryGetValue(StatType.ActionPoints, out var boost1))
            {
                foreach (var boost in boost1)
                {
                    actionPoints += boost.boostAmount;
                }
                playerActionPointsManager.UpdateMaxActionPoints(actionPoints);
            }
            
        }
        
        private void RemoveBoostDuration()
        {
            foreach (var statBoost in statBoosts)
            {
                for (int i = 0; i < statBoost.Value.Count; i++)
                {
                    statBoost.Value[i].duration -= 1;
                    if (statBoost.Value[i].duration <= 0)
                    {
                        statBoost.Value[i].boostAmount = -statBoost.Value[i].boostAmount;
                        UpdateStats();
                        statBoost.Value.RemoveAt(i);
                        return;
                    }
                }
            }
            UpdateStats();
        }
    }
}