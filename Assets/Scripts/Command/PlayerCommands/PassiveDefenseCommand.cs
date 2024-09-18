using System.Threading.Tasks;
using UnityEngine;

namespace Command.PlayerCommands
{
    public class PassiveDefenseCommand : PlayerCommand
    {
        public PassiveDefenseCommand(IFightingEntity player) : base(player)
        {
        }

        public override async Task Execute()
        {
            if (!player.commandPick.IsPreserved)
                player.Execute(null);
            await Awaitable.WaitForSecondsAsync(2f);
        }
    }
}