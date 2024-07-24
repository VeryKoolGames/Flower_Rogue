using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using Enemy;
using UnityEngine;

namespace Command
{
    public abstract class EnemyCommand : ICommand
    {
        protected IEnemyAttack attack;
        public List<Entity> targets = new();
        
        public EnemyCommand(IEnemyAttack attack)
        {
            this.attack = attack;
        }
        
        public void AddTarget(Entity target)
        {
            targets.Add(target);
        }

        public abstract Task Execute();
        
        public static T Create<T>(IEnemyAttack player, Entity[] targets) where T : EnemyCommand
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
        public EnemyAttackCommand(IEnemyAttack attack) : base(attack)
        {
        }

        public override async Task Execute()
        {
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    attack.Execute(target);
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
        public EnemyDefenseCommand(IEnemyAttack player) : base(player)
        {
        }

        public override async Task Execute()
        {
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    attack.Execute(target);
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
        public static EnemyCommand CreateCommand(IEnemyAttack player, Entity[] targets)
        {
            EnemyCommand command = null;

            if (player is EnemyAttack)
            {
                command = EnemyCommand.Create<EnemyAttackCommand>(player, targets);
            }
            else if (player is EnemyDefense)
            {
                command = EnemyCommand.Create<EnemyDefenseCommand>(player, targets);
            }
            return command;
        }
    }
}