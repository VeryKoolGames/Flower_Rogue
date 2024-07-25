using UnityEngine;

namespace ScriptableObjectScripts
{
    [CreateAssetMenu(fileName = "IntVar", menuName = "ScriptableObjects/IntVariable")]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}