using Player;
using UnityEngine;

namespace ScriptableObjectScripts
{
    [CreateAssetMenu(fileName = "PetalBoost", menuName = "ScriptableObjects/Boosts/PetalBoost")]
    public class PetalBoostSO : ScriptableObject
    {
        public string boostName;
        public PetalBoostType statType;
        public int boostAmount;
        public int duration;
    }
}