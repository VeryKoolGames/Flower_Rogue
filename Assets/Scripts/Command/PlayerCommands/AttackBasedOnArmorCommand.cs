using System.Threading.Tasks;
using UnityEngine;

namespace Command.PlayerCommands
{
    public class AttackBasedOnArmorCommand : PlayerCommand
    {
        public AttackBasedOnArmorCommand(IFightingEntity player) : base(player)
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
                        if (target != null)
                            player.Execute(target);
                    }
                }
                else
                {
                    player.RemovePetal();
                }
            }
            await Awaitable.WaitForSecondsAsync(2f);
        }
    }
}