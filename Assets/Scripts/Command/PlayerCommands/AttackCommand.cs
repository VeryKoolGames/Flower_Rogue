using System.Threading.Tasks;
using UnityEngine;

namespace Command.PlayerCommands
{
    public class AttackCommand : PlayerCommand
    {
        public AttackCommand(IFightingEntity player) : base(player)
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
            }

            await Awaitable.WaitForSecondsAsync(2f);
        }
    }
}