using System.Threading.Tasks;
using UnityEngine;

namespace Command.PlayerCommands
{
    public class BoostCommand : PlayerCommand
    {
        public BoostCommand(IFightingEntity target) : base(target)
        {
        }

        public override async Task Execute()
        {
            if (!player.commandPick.IsPersistent)
            {
                player.RemovePetal();
            }
            await Awaitable.WaitForSecondsAsync(2f);
        }
    }
}