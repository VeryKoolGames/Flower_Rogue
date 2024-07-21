using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using DefaultNamespace.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace Command
{
    public class CommandManager : MonoBehaviour
    {
        public IFightingEntity fightingEntity;
        public List<ICommand> commandList;
        [SerializeField] private OnCommandCreationListener onCommandCreationListener;
        readonly CommandInvoker commandInvoker = new();

        private void Start()
        {
            onCommandCreationListener.Response.AddListener(AddCommand);
            fightingEntity = GetComponent<IFightingEntity>();
            commandList = new List<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            if (commandList.Contains(command))
            {
                Debug.Log("Command already exists");
                commandList.Remove(command);
            }
            Debug.Log("Adding command to list: " + command);
            commandList.Add(command);
            Debug.Log("Command list count: " + commandList.Count);
        }
        
        public void DecorateCommand<TDecorator>(PlayerCommand command, int decoratorValue)
            where TDecorator : PetalDecorator.PetalDecorator
        {
            var decorator = (TDecorator)Activator.CreateInstance(typeof(TDecorator), decoratorValue);
            command.SetDecorator(decorator);
        }
        
        private void AddTargetToCommand(Entity target, ICommand command)
        {
            if (command is PlayerCommand playerCommand)
            {
                Debug.Log("Adding target to command" + target + " " + command);
                playerCommand.AddTarget(target);
            }
        }

        public void ExecuteCommand()
        {
            if (!CanExecuteCommand())
            {
                Debug.Log("No targets selected");
                return;
            }
            Debug.Log("Executing command in CommandManager " + commandList.Count);
            commandInvoker.ExecuteCommands(commandList);
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
        
        private void DecorateAndAddCommand<TCommand, TDecorator>(int decoratorValue, Entity[] targets)
            where TCommand : PlayerCommand
            where TDecorator : PetalDecorator.PetalDecorator
        {
            Debug.Log("Decorating Command");
            var command = PlayerCommand.Create<TCommand>(fightingEntity, targets);
            var decorator = (TDecorator)Activator.CreateInstance(typeof(TDecorator), decoratorValue);
            command.SetDecorator(decorator);
            AddCommand(command);
        }
    }

    public class CommandInvoker
    {
        public void ExecuteCommands(List<ICommand> commands)
        {
            foreach (var command in commands)
            {
                Debug.Log("Executing command: " + command);
                command.Execute();
            }
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
    }
}
