using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

namespace Command
{
    public abstract class EnemyCommand : ICommand
    {
        protected IFightingEntity player;
        public List<Entity> targets = new();
        
        public EnemyCommand(IFightingEntity player)
        {
            this.player = player;
        }
        
        public void AddTarget(Entity target)
        {
            targets.Add(target);
        }

        public abstract Task Execute();
        
        public static T Create<T>(IFightingEntity player, Entity[] targets) where T : EnemyCommand
        {
            var command = (T)System.Activator.CreateInstance(typeof(T), player);
            foreach (Entity target in targets)
            {
                command.AddTarget(target);
            }
            return command;
        }
    }

    public class EnemyAttackCommand : EnemyCommand
    {
        public EnemyAttackCommand(IFightingEntity player) : base(player)
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

    public class EnemyDefenseCommand : EnemyCommand
    {
        public EnemyDefenseCommand(IFightingEntity player) : base(player)
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

    public static class EnemyCommandFactory
    {
        public static EnemyCommand CreateCommand(IFightingEntity player, Entity[] targets)
        {
            EnemyCommand command = null;

            if (player is PetalAttack)
            {
                command = EnemyCommand.Create<EnemyAttackCommand>(player, targets);
            }
            else if (player is PetalDefense)
            {
                command = EnemyCommand.Create<EnemyDefenseCommand>(player, targets);
            }
            return command;
        }
    }
}