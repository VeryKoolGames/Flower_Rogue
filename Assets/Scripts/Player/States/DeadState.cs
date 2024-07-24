using UnityEngine;

namespace Player.States
{
    public class DeadState : PlayerState
    {
        public override void Enter()
        {
            Debug.Log("Player enters dead state.");
            player.gameObject.SetActive(false);
        }

        public override void Update()
        {
            // Do nothing
        }

        public override void Exit()
        {
            // Do nothing
        }
    }
}