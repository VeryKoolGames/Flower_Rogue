using System;
using UnityEngine;

namespace ScriptableObjectScripts
{
    [Serializable]
    public class PetalAttributes
    {
        public int cost;
        public string name;
        public string description;

        public PetalAttributes(int cost, string name, string description)
        {
            this.name = name;
            this.cost = cost;
            this.description = description;
        }
    }
    [CreateAssetMenu(fileName = "PetalSO", menuName = "ScriptableObjects/PetalSO")]
    public class PetalSO : ScriptableObject
    {
        public PetalAttributes petalAttributes;
    }
}