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
        
        public PlayerCommand(IFightingEntity player)
        {
            this.player = player;
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

        public override async Task Execute()
        {
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    Debug.Log("Attack Command " + " " + target);
                    player.Execute(target);
                }
            }
            else
            {
                Debug.LogError("No target found");
            }
            await Awaitable.WaitForSecondsAsync(2f);
        }
    }

    public class DefenseCommand : PlayerCommand
    {
        public DefenseCommand(IFightingEntity player) : base(player)
        {
        }

        public override async Task Execute()
        {
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    player.Execute(target);
                }
            }
            else
            {
                Debug.LogError("No target found");
            }
            await Awaitable.WaitForSecondsAsync(2f);
        }
    }

    public class UtilityCommand : PlayerCommand
    {
        public UtilityCommand(IFightingEntity player) : base(player)
        {
        }

        public override async Task Execute()
        {
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    player.Execute(target);
                }
            }
            else
            {
                Debug.LogError("No target found");
            }
            await Awaitable.WaitForSecondsAsync(2f);
        }
    }

    public static class CommandFactory
    {
        public static PlayerCommand CreateCommand(IFightingEntity player, Entity[] targets)
        {
            PlayerCommand command = null;

            if (player is PetalAttack)
            {
                command = PlayerCommand.Create<AttackCommand>(player, targets);
            }
            else if (player is PetalDefense)
            {
                command = PlayerCommand.Create<DefenseCommand>(player, targets);
            }
            else if (player is PetalUtility)
            {
                command = PlayerCommand.Create<UtilityCommand>(player, targets);
            }
            return command;
        }
    }
}