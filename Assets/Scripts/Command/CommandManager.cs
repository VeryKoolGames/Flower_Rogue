using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command.PlayerCommands;
using DefaultNamespace;
using DefaultNamespace.Events;
using Entities;
using Events;
using PetalAttacks;
using UnityEngine;

namespace Command
{
    public class CommandManager : MonoBehaviour
    {
        public static CommandManager Instance;
        public LinkedList<ICommand> commandList = new LinkedList<ICommand>();
        [SerializeField] private OnCommandCreationListener onCommandCreationListener;
        [SerializeField] private OnTargetUpdateListener onTargetUpdateListener;
        [SerializeField] private OnTurnEndEvent onTurnEndEvent;
        [SerializeField] private OnPetalDeathListener onPetalDeathListener;
        readonly CommandInvoker _commandInvoker = new();
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            onPetalDeathListener.Response.AddListener(RemoveCommand);
            onCommandCreationListener.Response.AddListener(AddCommand);
            onTargetUpdateListener.Response.AddListener(UpdateTarget);
        }

        private void OnDisable()
        {
            onCommandCreationListener.Response.RemoveListener(AddCommand);
            onTargetUpdateListener.Response.RemoveListener(UpdateTarget);
            onPetalDeathListener.Response.RemoveListener(RemoveCommand);
        }

        public void AddCommand(ICommand command)
        {
            if (commandList.Contains(command))
            {
                return;
            }
            var node = commandList.First;
            while (node != null)
            {
                if (node.Value == null)
                {
                    node.Value = command;
                    return;
                }
                if (node.Next != null && node.Value.IsPreserved && node.Next.Value == null)
                {
                    node.Next.Value = command;
                    return;
                }
                node = node.Next;
            }

            commandList.AddLast(command);
        }
        
        public void AddCommandAtIndex(ICommand command, int index)
        {
            if (commandList.Contains(command))
            {
                return;
            }
            var node = GetNodeAt(commandList, index);

            if (node != null)
            {
                node.Value = command;
            }
        }
        
        public void SwapCommands(ICommand command1, ICommand command2)
        {
            var node1 = commandList.Find(command1);
            var node2 = commandList.Find(command2);

            if (node1 != null && node2 != null)
            {
                (node1.Value, node2.Value) = (node2.Value, node1.Value);
            }
        }
        
        public void RemoveCommand(GameObject petal)
        {
            var command = petal.GetComponent<IFightingEntity>().commandPick;
            var node = commandList.Find(command);
            if (node != null)
            {
                commandList.Remove(node);
            }
        }

        private void UpdateTarget(ICommand command, Entity[] targets)
        {
            if (command is PlayerCommand playerCommand)
            {
                playerCommand.ClearTargets();
                foreach (var target in targets)
                {
                    AddTargetToCommand(target, command);
                }
            }
        }

        private void AddTargetToCommand(Entity target, ICommand command)
        {
            if (command is PlayerCommand playerCommand)
            {
                playerCommand.AddTarget(target);
            }
        }

        public async void ExecuteCommand()
        {
            if (!CanExecuteCommand())
            {
                return;
            }
            await _commandInvoker.ExecuteCommands(commandList);
            onTurnEndEvent.Raise();
        }

        public void ExecuteCommand(ICommand command)
        {
            _commandInvoker.ExecuteCommand(command);
        }

        public void ClearCommands()
        {
            var node = commandList.First;
            while (node != null && node.Value != null)
            {
                var nextNode = node.Next; // Store the next node because we may remove the current one
                if (!node.Value.IsPreserved)
                {
                    node.Value = null;
                }
                node = nextNode;
            }
        }

        private bool CanExecuteCommand()
        {
            foreach (var command in commandList)
            {
                if (command is AttackCommand playerCommand && playerCommand.targets.Count == 0)
                {
                    Debug.Log("No targets selected for: " + playerCommand);
                    return false;
                }
            }
            return true;
        }

        // Helper method to get node at a specific index in a LinkedList
        private LinkedListNode<T> GetNodeAt<T>(LinkedList<T> list, int index)
        {
            if (index < 0 || index >= list.Count) return null;

            var node = list.First;
            for (int i = 0; i < index; i++)
            {
                node = node.Next;
            }

            return node;
        }
    }

    public class CommandInvoker
    {
        public async Task ExecuteCommands(LinkedList<ICommand> commands)
        {
            foreach (var command in commands)
            {
                Debug.Log("Executing player command: " + command);
                await command.Execute();
            }
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
    }
}
