using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using DG.Tweening;
using Events;
using PetalAttacks;
using UnityEngine;

namespace PetalBehaviors
{
    public class PetalDragManager : MonoBehaviour
    {
        private List<PetalDrag> petals = new List<PetalDrag>();
        private List<ICommand> commands = new List<ICommand>();
        private PetalDrag currentPetal;
        public float swapDistance = 1f;
        [Header("Events")]
        [SerializeField] private OnPetalSpawnListener onPetalSpawnListener;
        [SerializeField] private OnPetalStartDraggingListener onPetalStartDraggingListener;
        [SerializeField] private OnPetalStoptDraggingListener onPetalEndDraggingListener;
        [SerializeField] private OnTurnEndListener onTurnEndListener;
        [SerializeField] private OnPetalDeathListener onPetalDeathListener;
        [SerializeField] private OnPetalSwapListener onPetalSwapListener;
        [SerializeField] private OnBoostListener onPetalBoostListener;
    
        void Awake()
        {
            onPetalSpawnListener.Response.AddListener(AddPetal);
            onPetalStartDraggingListener.Response.AddListener(OnPetalStartDragging);
            onPetalEndDraggingListener.Response.AddListener(OnPetalEndDragging);
            onTurnEndListener.Response.AddListener(ClearPetals);
            onPetalDeathListener.Response.AddListener(OnPetalDeath);
            onPetalSwapListener.Response.AddListener(AddPetalAtIndex);
            onPetalBoostListener.Response.AddListener(CheckForBoosts);
        }

        private void OnDisable()
        {
            onPetalSpawnListener.Response.RemoveListener(AddPetal);
            onPetalStartDraggingListener.Response.RemoveListener(OnPetalStartDragging);
            onPetalEndDraggingListener.Response.RemoveListener(OnPetalEndDragging);
            onTurnEndListener.Response.RemoveListener(ClearPetals);
            onPetalDeathListener.Response.RemoveListener(OnPetalDeath);
            onPetalSwapListener.Response.RemoveListener(AddPetalAtIndex);
            onPetalBoostListener.Response.RemoveListener(CheckForBoosts);
        }
    
        private void OnPetalStartDragging(PetalDrag petal)
        {
            currentPetal = petal;
        }
    
        private void ClearPetals()
        {
            petals.Clear();
            commands.Clear();
        }
    
        private void OnPetalEndDragging()
        {
            currentPetal = null;
            CheckForBoosts();
        }
    
        private void OnPetalDeath(GameObject petal)
        {
            petals.Remove(petal.GetComponent<PetalDrag>());
            commands.Remove(petal.GetComponent<IFightingEntity>().commandPick);
        }

        void Update()
        {
            if (currentPetal != null)
            {
                CheckProximity(currentPetal);
            }
        }
    
        public void CheckProximity(PetalDrag draggedPetal)
        {
            foreach (var petal in petals)
            {
                if (petal != draggedPetal && Vector3.Distance(petal.transform.position, draggedPetal.transform.position) < swapDistance)
                {
                    SwapPetalParent(draggedPetal, petal);
                    break;
                }
            }
        }
    
        private void SwapPetalParent(PetalDrag draggedPetal, PetalDrag targetPetal)
        {
            targetPetal.transform.SetParent(draggedPetal.originalParent);
            targetPetal.transform.DOMove(draggedPetal.originalParent.position, 0.2f).SetEase(Ease.OutQuad);
            draggedPetal.targetRotation = targetPetal.originalParent.rotation.eulerAngles;
            targetPetal.transform.DORotate(draggedPetal.originalParent.rotation.eulerAngles, 0.2f).SetEase(Ease.OutQuad);
            draggedPetal.originalParent = targetPetal.originalParent;
            draggedPetal.transform.SetParent(targetPetal.originalParent);
            targetPetal.originalParent = targetPetal.transform.parent;
            int place = draggedPetal.gameObject.GetComponent<PlayerMove>().placeInHand;
            draggedPetal.gameObject.GetComponent<PlayerMove>().placeInHand = targetPetal.gameObject.GetComponent<PlayerMove>().placeInHand;
            targetPetal.gameObject.GetComponent<PlayerMove>().placeInHand = place;
            SwapCommands(draggedPetal.gameObject.GetComponent<IFightingEntity>().commandPick, targetPetal.gameObject.GetComponent<IFightingEntity>().commandPick);
            SwapPetals(draggedPetal, targetPetal);
        }
    
        private void SwapCommands(ICommand command1, ICommand command2)
        {
            var index1 = commands.IndexOf(command1);
            var index2 = commands.IndexOf(command2);
            commands[index1] = command2;
            commands[index2] = command1;
            CommandManager.Instance.commandList = commands;
        }
        
        private void SwapPetals(PetalDrag petal1, PetalDrag petal2)
        {
            var index1 = petals.IndexOf(petal1);
            var index2 = petals.IndexOf(petal2);
            petals[index1] = petal2;
            petals[index2] = petal1;
        }

        private void CheckForBoosts()
        {
            foreach (var petal in petals)
            {
                PlayerMove playerMove = petal.gameObject.GetComponent<PlayerMove>();
                playerMove.boostCount = 0;
            }
            foreach (var petal in petals)
            {
                PlayerMove playerMove = petal.gameObject.GetComponent<PlayerMove>();
                if (playerMove is PetalBoost boost)
                {
                    Debug.Log("Boost at " + petals.IndexOf(petal));
                    if (boost.boostLeft)
                    {
                        if (petals.IndexOf(petal) > 0)
                        {
                            PlayerMove petalToBoost = petals[petals.IndexOf(petal) - 1].gameObject.GetComponent<PlayerMove>();
                            petalToBoost.boostCount += boost.boostAmount;
                        }
                    }
                    if (boost.boostRight)
                    {
                        if (petals.IndexOf(petal) < petals.Count - 1)
                        {
                            PlayerMove petalToBoost = petals[petals.IndexOf(petal) + 1].gameObject.GetComponent<PlayerMove>();
                            petalToBoost.boostCount += boost.boostAmount;
                        }
                    }
                }
            }
        }

        private void AddPetal(PetalDrag petal)
        {
            if (petals.Contains(petal))
            {
                Debug.Log("Petal already in list");
                return;
            }
            petals.Add(petal);
            commands.Add(petal.gameObject.GetComponent<IFightingEntity>().commandPick);
        }
        
        public void AddPetalAtIndex(PetalDrag petal, int index)
        {
            petals.Insert(index, petal);
            commands.Insert(index, petal.gameObject.GetComponent<IFightingEntity>().commandPick);
        }
    }
}
