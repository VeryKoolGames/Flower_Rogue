using UnityEngine;

namespace Player.States
{
    public class HighHealthState : PlayerState
    {
        public override void Enter()
        {
            Debug.Log("Player is in high health state.");
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