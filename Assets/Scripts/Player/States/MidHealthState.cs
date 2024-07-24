using UnityEngine;

namespace Player.States
{
    public class MidHealthState : PlayerState
    {
        public override void Enter()
        {
            Debug.Log("Player is in mid health state.");
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