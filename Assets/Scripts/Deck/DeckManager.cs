using UnityEngine;

namespace DefaultNamespace.Deck
{
    public class DeckManager : MonoBehaviour
    {
        public int maxPetals = 5;
        public static DeckManager instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            Debug.Log("Should instantiate first five petals there");
        }
    }
}