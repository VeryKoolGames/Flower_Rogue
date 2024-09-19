using UnityEngine;

namespace ScriptableObjectScripts
{
    [CreateAssetMenu(fileName = "StatBoost", menuName = "ScriptableObjects/Boosts/StatBoost")]
    public class PlayerBoostSO : ScriptableObject
    {
        public string boostName;
        public StatType statType;
        public int boostAmount;
        public int duration;
    }

    public enum StatType
    {
        Health,
        ActionPoints,
        Defense,
        Attack
    }
}