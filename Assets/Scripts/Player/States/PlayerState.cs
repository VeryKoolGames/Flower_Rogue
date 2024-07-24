namespace Player.States
{
    public abstract class PlayerState
    {
        protected Player player;
        public void Initialize(Player player)
        {
            this.player = player;
        }
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}