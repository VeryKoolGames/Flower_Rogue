using System.Threading.Tasks;
using UnityEngine;

namespace Command.PlayerCommands
{
    public class ScalingDefenseCommand : PlayerCommand
    {
        public ScalingDefenseCommand(IFightingEntity player) : base(player)
        {
        }

        public override async Task Execute()
        {
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    if (target != null && !player.commandPick.IsPreserved)
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
}