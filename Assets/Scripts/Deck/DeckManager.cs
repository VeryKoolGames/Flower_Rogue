using System;
using System.Collections.Generic;
using Combat;
using Command;
using DefaultNamespace.Events;
using DefaultNamespace.ScriptableObjectScripts;
using Events;
using Events.PlayerMoveEvents;
using PetalAttacks;
using PetalBehaviors;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Deck
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> petalSpawnPoints;
        [SerializeField] private DeckSO deckSO;
        [SerializeField] private Entities.Player player;
        private List<GameObject> petals = new List<GameObject>();
        [SerializeField] private OnTurnEndListener onTurnEndListener;
        [SerializeField] private OnDrawPetalListener onDrawPetalListener;
        [SerializeField] private PetalDragManager petalDragManager;
        [SerializeField] private OnBoostEvent onBoostEvent;
        [SerializeField] private TextMeshProUGUI petalDescritpion;
        [SerializeField] private OnTurnEndEvent onCombatStartEvent;
        [SerializeField] private PetalRedrawManager petalRedrawManager;
        
        private void Awake()
        {
            onDrawPetalListener.Response.AddListener(ReplacePetal);
            onTurnEndListener.Response.AddListener(SpawnPetals);
            petals.AddRange(deckSO.attackPetals);
            petals.AddRange(deckSO.defensePetals);
            petals.AddRange(deckSO.utilityPetals);
        }

        private void Start()
        {
            SpawnPetals();
        }

        private async void SpawnPetals()
        {
            CommandManager.Instance.ClearCommands();
            for (int i = 0; i < petalSpawnPoints.Count; i++)
            {
                if (petalSpawnPoints[i].childCount > 0)
                {
                    continue;
                }
                SpawnPetal(petalSpawnPoints[i], i);
            }
            await Awaitable.WaitForSecondsAsync(.2f);
            onBoostEvent.Raise();
            onCombatStartEvent.Raise();
        }
        
        private void SpawnPetal(Transform spawnPoint, int i)
        {
            Vector3 position = spawnPoint.position;
            Quaternion rotation = spawnPoint.rotation;
            GameObject obj = Instantiate(GetRandomPetal(), position, rotation);
            obj.transform.SetParent(spawnPoint);
            IFightingEntity petal = obj.GetComponent<IFightingEntity>();
            petal.Initialize(player);
            obj.GetComponent<PlayerMove>().placeInHand = i;
            obj.GetComponent<PetalDescriptionManager>().descriptionText = petalDescritpion;
            CommandManager.Instance.AddCommand(petal.commandPick);
        }
        
        private void ReplacePetal(GameObject petal)
        {
            Vector3 position = petal.transform.position;
            Quaternion rotation = petal.transform.rotation;
            GameObject obj = Instantiate(GetRandomPetal(), position, rotation);
            obj.GetComponent<PetalDescriptionManager>().descriptionText = petalDescritpion;
            obj.transform.SetParent(petal.transform.parent);
            IFightingEntity newPetal = obj.GetComponent<IFightingEntity>();
            newPetal.InitializeWithoutAdding(player);
            obj.GetComponent<PlayerMove>().placeInHand = petal.GetComponent<PlayerMove>().placeInHand;
            if (petal.GetComponent<PlayerMove>().isRedrawEnabled)
            {
                obj.GetComponent<PlayerMove>().isRedrawEnabled = true;
                petalRedrawManager.OnRedraw();
            }
            CommandManager.Instance.AddCommandAtIndex(newPetal.commandPick, obj.GetComponent<PlayerMove>().placeInHand);
            petalDragManager.AddPetalAtIndex(obj.GetComponent<PetalDrag>(), obj.GetComponent<PlayerMove>().placeInHand);
        }
        
        
        private GameObject GetRandomPetal()
        {
            return petals[Random.Range(0, petals.Count)];
        }
    }
}