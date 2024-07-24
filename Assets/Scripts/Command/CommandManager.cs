using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Events;
using UnityEngine;

namespace Command
{
    public class CommandManager : MonoBehaviour
    {
        public static CommandManager Instance;
        [SerializeField] private List<GameObject> petalPrefabs;
        public List<ICommand> commandList = new List<ICommand>();
        [SerializeField] private OnCommandCreationListener onCommandCreationListener;
        [SerializeField] private OnTargetUpdateListener onTargetUpdateListener;
        [SerializeField] private OnTurnEndEvent onTurnEndEvent;
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

        private void Start()
        {
            onCommandCreationListener.Response.AddListener(AddCommand);
            onTargetUpdateListener.Response.AddListener(UpdateTarget);
        }

        private void OnDisable()
        {
            onCommandCreationListener.Response.RemoveListener(AddCommand);
            onTargetUpdateListener.Response.RemoveListener(UpdateTarget);
        }

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
                return;
            }
            commandInvoker.ExecuteCommands(commandList, onTurnEndEvent);
        }
        
        private bool CanExecuteCommand()
        {
            foreach (var command in commandList)
            {
                if (command is PlayerCommand playerCommand)
                {
                    if (playerCommand.targets.Count == 0)
                    {
                        Debug.Log("No targets selected for : " + playerCommand);
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public class CommandInvoker
    {
        public async void ExecuteCommands(List<ICommand> commands, OnTurnEndEvent onTurnEndEvent)
        {
            foreach (var command in commands)
            {
                Debug.Log("Executing player command: " + command);
                await command.Execute();
            }
            onTurnEndEvent.Raise();
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
    }
}
