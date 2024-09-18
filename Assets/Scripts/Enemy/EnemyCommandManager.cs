using System.Collections.Generic;
using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using Entities;
using UnityEngine;

namespace Enemy
{
    public class EnemyCommandManager : MonoBehaviour
    {
        public static EnemyCommandManager Instance;
        public LinkedList<ICommand> commandList = new LinkedList<ICommand>();
        [SerializeField] private OnTurnEndEvent onEnemyTurnEndEvent;
        readonly CommandInvoker commandInvoker = new();
        [SerializeField] private OnEnemyDeathListener onEnemyDeathListener;
        [SerializeField] private OnTurnEndListener onPlayerTurnEndListener;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            onEnemyDeathListener.Response.AddListener(OnEnemyDeath);
            onPlayerTurnEndListener.Response.AddListener(ExecuteCommand);
        }
        
        private void OnDisable()
        {
            onEnemyDeathListener.Response.RemoveListener(OnEnemyDeath);
            onPlayerTurnEndListener.Response.RemoveListener(ExecuteCommand);
        }

        public void AddCommand(ICommand command)
        {
            var node = commandList.First;
            while (node != null)
            {
                if (!node.Value.IsPreserved)
                {
                    commandList.AddBefore(node, command);
                    return;
                }
                node = node.Next;
            }

            commandList.AddLast(command);
        }
        
        public void RemoveCommand(ICommand command)
        {
            commandList.Remove(command);
        }
        
        private void OnEnemyDeath(Entity enemy)
        {
            Debug.Log("Enemy death event received");
            ICommand command = enemy.entityGameObject.GetComponent<EnemyController>().currentCommand;
            RemoveCommand(command);
        }
        
        private void AddTargetToCommand(Entity target, ICommand command)
        {
            if (command is PlayerCommand playerCommand)
            {
                Debug.Log("Adding target to command" + target + " " + command);
                playerCommand.AddTarget(target);
            }
        }

        public async void ExecuteCommand()
        {
            if (!CanExecuteCommand())
            {
                Debug.Log("No targets selected");
                return;
            }
            await commandInvoker.ExecuteCommands(commandList);
            onEnemyTurnEndEvent.Raise();
        }
        
        private bool CanExecuteCommand()
        {
            foreach (var command in commandList)
            {
                if (command is PlayerCommand playerCommand)
                {
                    if (playerCommand.targets.Count == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}