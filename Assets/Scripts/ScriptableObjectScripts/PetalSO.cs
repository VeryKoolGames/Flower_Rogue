using System;
using UnityEngine;

namespace ScriptableObjectScripts
{
    [Serializable]
    public class PetalAttributes
    {
        public int activeValue;
        public int passiveValue;
        public int cost;
        public string name;
        public string description;
        public string warningDescription;

        public PetalAttributes(int cost, string name, string description, string warningDescription)
        {
            this.name = name;
            this.cost = cost;
            this.description = description;
            this.warningDescription = warningDescription;
        }
    }
    [CreateAssetMenu(fileName = "PetalSO", menuName = "ScriptableObjects/PetalSO")]
    public class PetalSO : ScriptableObject
    {
        public PetalAttributes petalAttributes;
    }
}