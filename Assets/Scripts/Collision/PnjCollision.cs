using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Collision
{
    public class PnjCollision : MonoBehaviour, IOnPlayerCollision
    {
        public bool CanExecuteAction { get; set; }
        [SerializeField] private GameObject popUpDialogue;
        private PlayerMovement _playerMovement;
        [SerializeField] private DialogueManager dialogueManager;
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

        public async void ExecuteAction()
        {
            _playerMovement.SetCanMove(false);
            List<string> dialogue = new List<string>
            {
                "Hello there you look nice today",
                "How are you?"
            };
            CanExecuteAction = false;
            await dialogueManager.StartDialogue(dialogue);
            CanExecuteAction = true;
            _playerMovement.SetCanMove(true);
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