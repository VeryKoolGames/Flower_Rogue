using System.Threading.Tasks;
using UnityEngine;

namespace Command.PlayerCommands
{
    public class RedrawCommand : PlayerCommand
    {
        public RedrawCommand(IFightingEntity player) : base(player)
        {
        }

        public override async Task Execute()
        {
            if (!player.commandPick.IsPersistent)
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
            }

            await Awaitable.WaitForSecondsAsync(2f);
        }
    }
}