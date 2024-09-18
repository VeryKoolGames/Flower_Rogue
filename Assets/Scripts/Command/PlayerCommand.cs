using System.Collections.Generic;
using System.Threading.Tasks;
using Command.PlayerCommands;
using DefaultNamespace;
using Entities;
using KBCore.Refs;
using PetalAttacks;

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

    public static class CommandFactory
    {
        public static PlayerCommand CreateCommand(IFightingEntity player, Entity[] targets)
        {
            PlayerCommand command = player switch
            {
                PetalAttack => PlayerCommand.Create<AttackCommand>(player, targets),
                PetalDefense => PlayerCommand.Create<DefenseCommand>(player, targets),
                PetalDraw => PlayerCommand.Create<RedrawCommand>(player, targets),
                PetalBoost => PlayerCommand.Create<BoostCommand>(player, targets),
                PetalAttackScaling => PlayerCommand.Create<ScalingAttackCommand>(player, targets),
                PetalDefenseScaling => PlayerCommand.Create<ScalingDefenseCommand>(player, targets),
                PetalBoostActionPointsNextTurn => PlayerCommand.Create<BoostActionPointsCommand>(player, targets),
                PetalPassiveDefense => PlayerCommand.Create<PassiveDefenseCommand>(player, targets),
                PetalAttackBasedOnArmor => PlayerCommand.Create<AttackBasedOnArmorCommand>(player, targets),
                _ => null
            };
            return command;
        }
    }
}