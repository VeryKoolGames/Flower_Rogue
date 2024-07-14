using System.Threading.Tasks;
using UnityEngine;

namespace Command
{
    public abstract class PlayerCommand : ICommand
    {
        public IEntity player;
        public PlayerCommand(IEntity player)
        {
            this.player = player;
        }
        public abstract Task Execute();
        
        public static T Create<T>(IEntity player) where T : PlayerCommand
        {
            return (T) System.Activator.CreateInstance(typeof(T), player);
        }
    }
    
    public class AttackCommand : PlayerCommand
    {
        public AttackCommand(IEntity player) : base(player)
        {
        }

        public override Task Execute()
        {
            player.Attack();
            // await Awaitable.
            return Task.CompletedTask;
        }
    }
    public class DefenseCommand : PlayerCommand
    {
        public DefenseCommand(IEntity player) : base(player)
        {
        }

        public override Task Execute()
        {
            player.Defense();
            // await Awaitable.
            return Task.CompletedTask;
        }
    }
    public class UtilityCommand : PlayerCommand
    {
        public UtilityCommand(IEntity player) : base(player)
        {
        }

        public override Task Execute()
        {
            player.Utility();
            // await Awaitable.
            return Task.CompletedTask;
        }
    }
}