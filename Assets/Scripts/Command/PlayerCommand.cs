using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

namespace Command
{
    public abstract class PlayerCommand : ICommand
    {
        protected IFightingEntity player;
        public List<Entity> targets = new();
        protected PetalDecorator.PetalDecorator decorator;
        
        public PlayerCommand(IFightingEntity player)
        {
            this.player = player;
        }

        public void SetDecorator(PetalDecorator.PetalDecorator decorator)
        {
            this.decorator = decorator;
            this.targets = new List<Entity>();
        }
        
        public void AddTarget(Entity target)
        {
            targets.Add(target);
        }
        
        public void ClearTargets()
        {
            targets.Clear();
        }

        public abstract Task Execute();
        
        public static T Create<T>(IFightingEntity player, Entity[] targets) where T : PlayerCommand
        {
            var command = (T)System.Activator.CreateInstance(typeof(T), player);
            foreach (Entity target in targets)
            {
                command.AddTarget(target);
            }
            return command;
        }
    }

    public class AttackCommand : PlayerCommand
    {
        public AttackCommand(IFightingEntity player) : base(player)
        {
        }

        public override Task Execute()
        {
            int value = decorator?.Play() ?? 0;
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    Debug.Log("Attack Command " + " " + target);
                    player.Attack(target);
                }
            }
            else
            {
                Debug.LogError("No target found");
            }
            return Task.CompletedTask;
        }
    }

    public class DefenseCommand : PlayerCommand
    {
        public DefenseCommand(IFightingEntity player) : base(player)
        {
        }

        public override Task Execute()
        {
            int value = decorator?.Play() ?? 0;
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    player.Defense(target);
                }
            }
            else
            {
                Debug.LogError("No target found");
            }
            return Task.CompletedTask;
        }
    }

    public class UtilityCommand : PlayerCommand
    {
        public UtilityCommand(IFightingEntity player) : base(player)
        {
        }

        public override Task Execute()
        {
            int value = decorator?.Play() ?? 0;
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    player.Utility(target);
                }
            }
            else
            {
                Debug.LogError("No target found");
            }
            return Task.CompletedTask;
        }
    }
}