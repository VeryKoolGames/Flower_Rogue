using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using KBCore.Refs;
using PetalAttacks;
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
        public bool IsPreserved { get; set; }

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
                    if (target != null && target != player)
                        player.Execute(target);
                    else
                    {
                        player.RemovePetal();
                    }
                }
            }
            else
            {
                Debug.LogError("No target found");
            }
            await Awaitable.WaitForSecondsAsync(2f);
        }
    }
    
    public class ScalingCommand : PlayerCommand
    {
        public ScalingCommand(IFightingEntity player) : base(player)
        {
            this.IsPreserved = true;
        }

        public override async Task Execute()
        {
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    if (target != null)
                        player.Execute(target);
                    else
                    {
                        player.RemovePetal();
                    }
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
    
    public class BoostCommand : PlayerCommand
    {
        public BoostCommand(IFightingEntity target) : base(target)
        {
        }

        public override async Task Execute()
        {
            player.RemovePetal();
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
            else if (player is PetalBoost)
            {
                command = PlayerCommand.Create<BoostCommand>(player, targets);
            }
            else if (player is PetalScaling)
            {
                command = PlayerCommand.Create<ScalingCommand>(player, targets);
            }
            return command;
        }
    }
}