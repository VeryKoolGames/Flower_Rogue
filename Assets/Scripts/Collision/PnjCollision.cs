using System;
using Player;
using UnityEngine;

namespace Collision
{
    public class PnjCollision : MonoBehaviour, IOnPlayerCollision
    {
        public bool CanExecuteAction { get; set; }
        [SerializeField] private GameObject popUpDialogue;
        private PlayerMovement _playerMovement;
        public void OnPlayerCollisionEnter(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
            popUpDialogue.SetActive(true);
            CanExecuteAction = true;
        }

        public void OnPlayerCollisionExit()
        {
            popUpDialogue.SetActive(false);
            CanExecuteAction = false;
        }

        public void ExecuteAction()
        {
            _playerMovement.SetCanMove(false);
            Debug.Log("Pnj Dialogue should Open here");
        }

        private void Update()
        {
            if (!CanExecuteAction)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExecuteAction();
            }
        }
    }
}