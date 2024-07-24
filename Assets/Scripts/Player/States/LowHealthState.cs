using UnityEngine;

namespace Player.States
{
    public class LowHealthState : PlayerState
    {
        public override void Enter()
        {
            Debug.Log("Player is in low health state.");
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
            // Do nothing
        }
    }
}