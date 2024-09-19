using System.Threading.Tasks;
using UnityEngine;

namespace Command.PlayerCommands
{
    public class ScalingAttackCommand : PlayerCommand
    {
        public ScalingAttackCommand(IFightingEntity player) : base(player)
        {
        }

        public override async Task Execute()
        {
            if (targets.Count > 0)
            {
                foreach (var target in targets)
                {
                    if (target != null || !player.commandPick.IsPersistent)
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