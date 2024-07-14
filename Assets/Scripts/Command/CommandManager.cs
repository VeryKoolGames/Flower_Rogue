using System;
using System.Collections.Generic;
using UnityEngine;

namespace Command
{
    public class CommandManager : MonoBehaviour
    {
        public IEntity Entity;
        public List<ICommand> commandList = new ();
        
        readonly CommandInvoker commandInvoker = new ();

        private void Start()
        {
            Entity = GetComponent<IEntity>();

            commandList = new List<ICommand>
            {
                PlayerCommand.Create<AttackCommand>(Entity),
                PlayerCommand.Create<DefenseCommand>(Entity),
                PlayerCommand.Create<UtilityCommand>(Entity)
            };
        }
        
        public void AddCommand(ICommand command)
        {
            commandList.Add(command);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ExecuteCommand(commandList);
            }
        }

        private void ExecuteCommand(List<ICommand> commands)
        {
            commandInvoker.ExecuteCommands(commands);
        }
    }
    

    public class CommandInvoker
    {
        public void ExecuteCommands(List<ICommand> commands)
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }
    }
}