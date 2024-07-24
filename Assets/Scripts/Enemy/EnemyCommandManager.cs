using System.Collections.Generic;
using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using UnityEngine;

namespace Enemy
{
    public class EnemyCommandManager : MonoBehaviour
    {
        public static EnemyCommandManager Instance;
        public List<ICommand> commandList = new List<ICommand>();
        [SerializeField] private OnTurnEndEvent onEnemyTurnEndEvent;
        readonly CommandInvoker commandInvoker = new();
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        // private void Start()
        // {
        //     onCommandCreationListener.Response.AddListener(AddCommand);
        // }
        //
        // private void OnDisable()
        // {
        //     onCommandCreationListener.Response.RemoveListener(AddCommand);
        // }

        public void AddCommand(ICommand command)
        {
            commandList.Add(command);
        }
        
        private void UpdateTarget(ICommand command, Entity[] targets)
        {
            Debug.Log("Updating target in CommandManager");
            if (command is PlayerCommand playerCommand)
            {
                playerCommand.ClearTargets();
                foreach (var target in targets)
                {
                    AddTargetToCommand(target, command);
                }
            }
        }
        
        // public void DecorateCommand<TDecorator>(PlayerCommand command, int decoratorValue)
        //     where TDecorator : PetalDecorator.PetalDecorator
        // {
        //     var decorator = (TDecorator)Activator.CreateInstance(typeof(TDecorator), decoratorValue);
        //     command.SetDecorator(decorator);
        // }
        
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
            commandInvoker.ExecuteCommands(commandList, onEnemyTurnEndEvent);
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