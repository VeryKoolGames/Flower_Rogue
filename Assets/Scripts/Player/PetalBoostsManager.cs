using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using PetalAttacks;
using ScriptableObjectScripts;
using UnityEngine;

namespace Player
{
    public enum PetalBoostType
    {
        Damage,
        Persistent,
    }
    public class PetalBoostData
    {
        public int boostAmount;
        public int duration;
        public PetalBoostSO petalBoostSo;

        public PetalBoostData(int amount, int duration, PetalBoostSO petalBoostSo)
        {
            this.boostAmount = amount;
            this.duration = duration;
            this.petalBoostSo = petalBoostSo;
        }
    }
    public class PetalBoostsManager : MonoBehaviour
    {
        private int dmgBoost;
        private Dictionary<PetalBoostType, List<PetalBoostData>> statBoosts = new Dictionary<PetalBoostType, List<PetalBoostData>>();
        private Dictionary<PetalBoostType, List<PetalBoostData>> statBoostsBuffer = new Dictionary<PetalBoostType, List<PetalBoostData>>();
        private PlayerMove _playerMove;
        private ICommand _command;
        
        // private void Awake()
        // {
        //     onTurnEndListener.Response.AddListener(RemoveBoostDuration);
        // }
        //
        public void Initialize(PlayerMove playerMove, ICommand command)
        {
            this._playerMove = playerMove;
            this._command = command;
        }
        //
        // private void OnDestroy()
        // {
        //     onTurnEndListener.Response.RemoveListener(RemoveBoostDuration);
        // }

        public void ApplyBoost(PetalBoostSO boost)
        {
            // If the move is already a persistent one we do not want to add another persistent boost
            if (boost.statType == PetalBoostType.Persistent && _playerMove is IFightingEntity fightingEntity)
            {
                fightingEntity.commandPick.IsPersistent = true;
            }
            if (!statBoosts.ContainsKey(boost.statType))
            {
                statBoosts[boost.statType] = new List<PetalBoostData>();
            }
            
            if (boost.statType == PetalBoostType.Damage)
            {
                if (_playerMove is PlayerAttackMove playerAttackMove)
                {
                    playerAttackMove.petalBoostUI.ApplyBoostingEffect();
                    playerAttackMove.boostCount += boost.boostAmount;
                }
            }

            var newBoost = new PetalBoostData(boost.boostAmount, boost.duration, boost);
            statBoosts[boost.statType].Add(newBoost);
        }

        public void RemoveBoost(PetalBoostSO petalBoostSo)
        {
            foreach (var statBoost in statBoostsBuffer)
            {
                for (int i = 0; i < statBoost.Value.Count; i++)
                {
                    if (statBoost.Value[i].petalBoostSo == petalBoostSo)
                    {
                        RemoveBoost(statBoost.Key, statBoost.Value[i]);
                        statBoost.Value.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        public void ClearBoosts()
        {
            // Create a list to store the keys you will modify
            List<PetalBoostType> keysToClear = new List<PetalBoostType>();

            // First, collect the keys of the boosts to clear, but don't modify the collection yet
            foreach (var statBoost in statBoosts)
            {
                if (statBoost.Key == PetalBoostType.Damage)
                {
                    if (_playerMove is PlayerAttackMove playerAttackMove)
                    {
                        playerAttackMove.petalBoostUI.RemoveBoostingEffect();
                        playerAttackMove.boostCount = 0;
                    }
                }
                else if (statBoost.Key == PetalBoostType.Persistent)
                {
                    _command.IsPersistent = false;
                }

                // Add the key to the list for clearing after iteration
                keysToClear.Add(statBoost.Key);
            }

            // Now, iterate through the collected keys and clear the values
            foreach (var key in keysToClear)
            {
                statBoosts[key].Clear(); // Clear the boost effects for this key
            }
        }

        
        private void CleanBuffer()
        {
            foreach (var statBoost in statBoostsBuffer)
            {
                statBoost.Value.Clear();
            }
        }
        
        private void AddBoostsFromBuffer()
        {
            foreach (var statBoost in statBoostsBuffer)
            {
                if (!statBoosts.ContainsKey(statBoost.Key))
                {
                    statBoosts[statBoost.Key] = new List<PetalBoostData>();
                }
                statBoosts[statBoost.Key].AddRange(statBoost.Value);
            }
            CleanBuffer();
        }

        private void UpdateStats()
        {
            dmgBoost = 0;
            if (statBoosts.TryGetValue(PetalBoostType.Damage, out var statBoost))
            {
                foreach (var boost in statBoost)
                {
                    dmgBoost += boost.boostAmount;
                }
                if (_playerMove is PlayerAttackMove playerAttackMove)
                {
                    playerAttackMove.boostCount = dmgBoost;
                }
            }

            if (statBoosts.TryGetValue(PetalBoostType.Persistent, out var boost1))
            {
                foreach (var boost in boost1)
                {
                    _command.IsPersistent = true;
                }
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
                        RemoveBoost(statBoost.Key, statBoost.Value[i]);
                        UpdateStats();
                        statBoost.Value.RemoveAt(i);
                        return;
                    }
                }
            }
            UpdateStats();
        }

        private void RemoveBoost(PetalBoostType statBoostKey, PetalBoostData statBoostData)
        {
            int dmgToSubtract = 0;
            switch (statBoostKey)
            {
                case PetalBoostType.Damage:
                    dmgToSubtract += statBoostData.boostAmount;
                    if (_playerMove is PlayerAttackMove playerAttackMove)
                    {
                        playerAttackMove.boostCount -= dmgToSubtract;
                    }
                    break;
                case PetalBoostType.Persistent:
                    _command.IsPersistent = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}