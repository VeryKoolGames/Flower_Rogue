using Player;

namespace Collision
{
    public interface IOnPlayerCollision
    {
        public bool CanExecuteAction { get; set; }
        public void OnPlayerCollisionEnter(PlayerMovement playerMovement);
        public void OnPlayerCollisionExit();
        public void ExecuteAction();
    }
}