namespace Player.States
{
    public abstract class PlayerState
    {
        protected Entities.Player player;
        public void Initialize(Entities.Player player)
        {
            this.player = player;
        }
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}