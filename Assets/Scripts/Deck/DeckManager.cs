using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using DefaultNamespace.ScriptableObjectScripts;
using Events.PlayerMoveEvents;
using UnityEngine;

namespace Deck
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> petalSpawnPoints;
        [SerializeField] private DeckSO deckSO;
        [SerializeField] private Player.Player player;
        private List<GameObject> petals = new List<GameObject>();
        [SerializeField] private OnTurnEndListener onTurnEndListener;
        [SerializeField] private OnDrawPetalListener onDrawPetalListener;
        
        private void Start()
        {
            onDrawPetalListener.Response.AddListener(ReplacePetal);
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
                SpawnPetal(petalSpawnPoints[i]);
            }
        }
        
        private void SpawnPetal(Transform spawnPoint)
        {
            Vector3 position = spawnPoint.position;
            Quaternion rotation = spawnPoint.rotation;
            GameObject obj = Instantiate(GetRandomPetal(), position, rotation);
            obj.transform.SetParent(spawnPoint);
            IFightingEntity petal = obj.GetComponent<IFightingEntity>();
            petal.Initialize(player);
            CommandManager.Instance.AddCommand(petal.commandPick);
        }
        
        private void ReplacePetal(GameObject petal)
        {
            Vector3 position = petal.transform.position;
            Quaternion rotation = petal.transform.rotation;
            GameObject obj = Instantiate(GetRandomPetal(), position, rotation);
            obj.transform.SetParent(petal.transform.parent);
            IFightingEntity newPetal = obj.GetComponent<IFightingEntity>();
            newPetal.Initialize(player);
            CommandManager.Instance.AddCommand(newPetal.commandPick);
        }
        
        
        private GameObject GetRandomPetal()
        {
            return petals[Random.Range(0, petals.Count)];
        }
    }
}