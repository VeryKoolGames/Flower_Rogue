using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.ScriptableObjectScripts
{
    [CreateAssetMenu(fileName = "New Deck", menuName = "ScriptableObjects/Deck")]
    public class DeckSO : ScriptableObject
    {
        public List<GameObject> attackPetals = new List<GameObject>();
        public List<GameObject> defensePetals = new List<GameObject>();
        public List<GameObject> utilityPetals = new List<GameObject>();
    }
}