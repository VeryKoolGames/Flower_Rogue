using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using DefaultNamespace.ScriptableObjectScripts;
using UnityEngine;

namespace DefaultNamespace.Deck
{
    public class DeckManager : MonoBehaviour
    {
        public int maxPetals = 6;
        [SerializeField] private List<Transform> petalSpawnPoints;
        [SerializeField] private DeckSO deckSO;
        [SerializeField] private Player player;
        private List<GameObject> petals = new List<GameObject>();
        [SerializeField] private OnTurnEndListener onTurnEndListener;
        
        private void Start()
        {
            onTurnEndListener.Response.AddListener(SpawnPetals);
            petals.AddRange(deckSO.attackPetals);
            petals.AddRange(deckSO.defensePetals);
            petals.AddRange(deckSO.utilityPetals);
            SpawnPetals();
        }
        
        private void SpawnPetals()
        {
            CommandManager.Instance.commandList.Clear();
            for (int i = 0; i < petalSpawnPoints.Count; i++)
            {
                Vector3 position = petalSpawnPoints[i].position;
                Quaternion rotation = petalSpawnPoints[i].rotation;
                GameObject obj = Instantiate(GetRandomPetal(), position, rotation);
                obj.transform.SetParent(petalSpawnPoints[i]);
                IFightingEntity petal = obj.GetComponent<IFightingEntity>();
                petal.Initialize(player);
                CommandManager.Instance.AddCommand(petal.commandPick);
            }
        }
        
        
        private GameObject GetRandomPetal()
        {
            return petals[Random.Range(0, petals.Count)];
        }
    }
}