namespace Player.States
{
    public class PlayerStateMachine
    {
        private PlayerState currentState;

        public PlayerState CurrentState => currentState;

        public void ChangeState(PlayerState newState)
        {
            if (newState == currentState)
            {
                return;
            }
            
            if (currentState != null)
            {
                currentState.Exit();
            }
        
            currentState = newState;
        
            if (currentState != null)
            {
                currentState.Enter();
            }
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }
    }
}